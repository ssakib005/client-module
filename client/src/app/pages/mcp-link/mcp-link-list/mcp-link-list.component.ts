import { Component, OnInit } from '@angular/core';
import { RestService } from '../../../services/rest.service';
import { ToastrService } from 'ngx-toastr';
import { MCPLink } from '../../../model/mcpLink.model';

@Component({
  selector: 'app-mcp-link-list',
  templateUrl: './mcp-link-list.component.html',
  styleUrls: ['./mcp-link-list.component.css'],
})
export class MCPLinkListComponent implements OnInit {
  list: MCPLink[];

  constructor(private rest: RestService, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.fetchMCPLinkList();
  }

  fetchMCPLinkList(): void {
    this.rest
      .fetchMCPLinkList()
      .subscribe((response) => (this.list = response));
  }

  deleteMCPLink(id): void {
    this.rest.deleteMCPLinkById(id).subscribe(
      (response) => {
        if (response) {
          this.toastr.success('MCP Link Deleted Successfully', 'MCP Link');
          this.fetchMCPLinkList();
        } else {
          this.toastr.error('MCP Link not found', 'MCP Link');
        }
      },
      (error) => {
        this.toastr.error('Something wrong! Please try again later', 'MCP Link');
      }
    );
  }
}
