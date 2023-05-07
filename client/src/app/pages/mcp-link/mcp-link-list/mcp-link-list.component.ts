import { Component, OnInit } from '@angular/core';
import { RestService } from '../../../services/rest.service';
import { ToastrService } from 'ngx-toastr';
import { MCPBoard } from '../../../model/mcpBoard.model';

@Component({
  selector: 'app-mcp-link-list',
  templateUrl: './mcp-link-list.component.html',
  styleUrls: ['./mcp-link-list.component.css'],
})
export class MCPLinkListComponent implements OnInit {
  list: MCPBoard[];

  constructor(private rest: RestService, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.fetchMCPBoardList();
  }

  fetchMCPBoardList(): void {
    this.rest
      .fetchMCPBoardList()
      .subscribe((response) => (this.list = response));
  }

  deleteMCPBoard(userId): void {
    this.rest.deleteMCPBoardById(userId).subscribe(
      (response) => {
        if (response) {
          this.toastr.success('MCP Board Deleted Successfully', 'MCP Board');
          this.fetchMCPBoardList();
        } else {
          this.toastr.error('MCP Board not found', 'User');
        }
      },
      (error) => {
        this.toastr.error('Something wrong! Please try again later', 'MCP Board');
      }
    );
  }
}
