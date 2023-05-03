import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RestService } from '../../../services/rest.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-mcp-board-create',
  templateUrl: './mcp-board-create.component.html',
  styleUrls: ['./mcp-board-create.component.css'],
})
export class MCPBoardCreateComponent implements OnInit {

  mcp!: FormGroup;
  id: string = undefined;
  isCreate: boolean = true;
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private rest: RestService,
    private toastr: ToastrService
  ) {
    this.id = this.route.snapshot.params['id'];

    if (this.id !== undefined) {
      this.isCreate = false;
      rest.fetchMCPBoardById(this.id).subscribe((response) => {
        this.mcp.setValue({
          id: response.id,
          name: response.name,
          description: response.description
        });
      });
    }
  }

  ngOnInit(): void {
    this.mcp = this.fb.group({
      id: '',
      name: ['', Validators.required],
      description: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.mcp.invalid) {
      return;
    }

    var data = this.mcp.value;

    if (this.id === undefined) {
      data.id = '';
      this.rest.createMCPBoard(data).subscribe(
        () => {
          this.toastr.success(
            'MCP Board Created Successfully',
            'MCP Board'
          );
          setTimeout(() => {
            this.router.navigate(['/pages/mcp-board/list']);
          }, 2000);
        },
        (error) => {
          this.toastr.error(error.error, 'MCP Board');
        }
      );
    } else {
      this.rest.updateMCPBoard(data).subscribe(
        () => {
          this.toastr.success('MCP Board Updated Successfully', 'MCP Board');
          setTimeout(() => {
            this.router.navigate(['/pages/mcp-board/list']);
          }, 2000);
        },
        (error) => {
          this.toastr.error(error.error, 'MCP Board');
        }
      );
    }
  }
}
