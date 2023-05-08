import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RestService } from '../../../services/rest.service';
import { Observable } from 'rxjs';
import { MineInformation } from '../../../model/mineInformation.model';
import { SiteInformation } from '../../../model/siteInformation.model';
import { FunctionalLocation } from '../../../model/functional.model';
import { MCPBoard } from '../../../model/mcpBoard.model';

@Component({
  selector: 'app-mcp-link-create',
  templateUrl: './mcp-link-create.component.html',
  styleUrls: ['./mcp-link-create.component.css'],
})
export class MCPLinkCreateComponent implements OnInit {

  mcp!: FormGroup;
  id: string = undefined;
  isCreate: boolean = true;
  mineInformationList: MineInformation[] =[];
  siteInformationList: SiteInformation[] =[];
  functionalLocationList: FunctionalLocation[] =[];
  mcpBoardList: MCPBoard[] =[];

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
      rest.fetchMCPLinkById(this.id).subscribe((response) => {

        this.rest
        .fetchSiteInformationListByMineInformationId(response.mineInformationId)
        .subscribe((response) => (
          this.siteInformationList = response));
        
        this.rest
      .fetchFunctionalLocationListBySiteInformationId(response.siteInformationId)
      .subscribe((response) => (
        this.functionalLocationList = response));
  
        this.mcp.get('siteInformationId').setValue("");
        this.mcp.setValue({
          id: response.id,
          mineInformationId: response.mineInformationId,
          siteInformationId: response.siteInformationId,
          mcpBoardId: response.mcpBoardId,
          panel: response.panel,
          functionalLocationId: response.functionalLocationId
        });
      });
    }
  }

  ngOnInit(): void {
    this.fetchMineInformationList();
    this.fetchMCPBoardList();

    this.mcp = this.fb.group({
      id: '',
      mineInformationId: ['', Validators.required],
      siteInformationId: ['', Validators.required],
      mcpBoardId: ['', Validators.required],
      panel: ['', Validators.required],
      functionalLocationId: ['', Validators.required]
    });
  }

  fetchMineInformationList(): void {
    this.rest
      .fetchMineInformationList()
      .subscribe((response) => (this.mineInformationList = response.data));
  }
  fetchMCPBoardList(): void {
    this.rest
      .fetchMCPBoardList()
      .subscribe((response) => (this.mcpBoardList = response));
  }

  onSubmit() {
    if (this.mcp.invalid) {
      return;
    }

    var data = this.mcp.value;

    if (this.id === undefined) {
      data.id = '';
      data.link = '';
      this.rest.createMCPLink(data).subscribe(
        () => {
          this.toastr.success(
            'MCP Link Created Successfully',
            'MCP Link'
          );
          setTimeout(() => {
            this.router.navigate(['/pages/mcp-link/list']);
          }, 2000);
        },
        (error) => {
          this.toastr.error(error.error, 'MCP Link');
        }
      );
    } else {
      data.link = '';
      this.rest.updateMCPLink(data).subscribe(
        () => {
          this.toastr.success('MCP Link Updated Successfully', 'MCP Link');
          setTimeout(() => {
            this.router.navigate(['/pages/mcp-link/list']);
          }, 2000);
        },
        (error) => {
          this.toastr.error(error.error, 'MCP Link');
        }
      );
    }
  }

  getSiteInformationList() {
    if (this.mcp.get('mineInformationId').value == null) {
      return;
    }
    else {

      this.rest
      .fetchSiteInformationListByMineInformationId(this.mcp.get('mineInformationId').value)
      .subscribe((response) => (
        this.siteInformationList = response));

      this.mcp.get('siteInformationId').setValue("");
    }
  }

  getFunctionalLocationList() {
    if (this.mcp.get('siteInformationId').value == null) {
      return;
    }
    else {
      this.rest
      .fetchFunctionalLocationListBySiteInformationId(this.mcp.get('siteInformationId').value)
      .subscribe((response) => (
        this.functionalLocationList = response));

      this.mcp.get('functionalLocationId').setValue("");
    }
  }

}
