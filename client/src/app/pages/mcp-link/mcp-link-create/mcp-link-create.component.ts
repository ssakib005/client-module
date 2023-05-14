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
import { MCPLink } from '../../../model/mcpLink.model';

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
  selectedList: MCPLink[] = [];
  randomId: any;

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
    var data = this.selectedList;

    if (this.id === undefined) {
      this.rest.createMCPLink(data).subscribe(
        () => {
          this.toastr.success(
            'MCP Link Created Successfully',
            'MCP Link'
          );
          setTimeout(() => {
           location.reload();
          }, 2000);
        },
        (error) => {
          this.toastr.error(error.error, 'MCP Link');
        }
      );
    } else {
      this.rest.updateMCPLink(data).subscribe(
        () => {
          this.toastr.success('MCP Link Updated Successfully', 'MCP Link');
          setTimeout(() => {
            location.reload();
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

      this.selectedList = [];
      this.rest
      .fetchSiteInformationListByMineInformationId(this.mcp.get('mineInformationId').value)
      .subscribe((response) => (
        this.siteInformationList = response));

      this.mcp.get('siteInformationId').setValue("");
    }
  }

  getMCPLinkANDFunctionalLocationList() {
    if (this.mcp.get('siteInformationId').value == null) {
      return;
    }
    else {
      this.rest
      .fetchFunctionalLocationListBySiteInformationId(this.mcp.get('siteInformationId').value)
      .subscribe((response) => (
        this.functionalLocationList = response));

      this.mcp.get('functionalLocationId').setValue("");

      this.rest
      .fetchMCPLinkListByMineANDSiteInformationId(this.mcp.get('mineInformationId').value,this.mcp.get('siteInformationId').value)
      .subscribe((response) => (
        this.selectedList = response));

    }
  }

  deleteMCPLink(mcpLink: MCPLink) {
    const index = this.selectedList.indexOf(mcpLink);
		if (index >= 0) {
			this.selectedList.splice(index, 1);
		}
  }

  editMCPLink(mcpLink: MCPLink) {
		if (mcpLink != null) {
      this.randomId = mcpLink.id;
      this.mcp.setValue({
        id: mcpLink.id,
        mineInformationId: mcpLink.mineInformationId,
        siteInformationId: mcpLink.siteInformationId,
        mcpBoardId: mcpLink.mcpBoardId,
        panel: mcpLink.panel,
        functionalLocationId: mcpLink.functionalLocationId
      });
		}
  }

  addMCPLink() {
    if (this.mcp.get('mineInformationId').value == null
    || this.mcp.get('siteInformationId').value == null
    || this.mcp.get('mcpBoardId').value == null
    || this.mcp.get('panel').value == null
    || this.mcp.get('functionalLocationId').value == null
    ) {
      return;
    }
    else {
      let selectedMineInformation = this.mineInformationList?.find(x => x.id === this.mcp.get('mineInformationId').value);
      let selectedSiteInformation = this.siteInformationList?.find(x => x.id === this.mcp.get('siteInformationId').value);
      let selectedMCPBoard = this.mcpBoardList?.find(x => x.id === this.mcp.get('mcpBoardId').value);
      let panel = this.mcp.get('panel').value;
      let selectedFunctionalLocation = this.functionalLocationList?.find(x => x.id === this.mcp.get('functionalLocationId').value);

      if(this.randomId != undefined)
      {
        let editedItem = this.selectedList?.find(x => x.id === this.randomId);
        editedItem.mineInformationId = selectedMineInformation.id;
        editedItem.mineInformationName = selectedMineInformation.name;
        editedItem.siteInformationId = selectedSiteInformation.id;
        editedItem.siteInformationName = selectedSiteInformation.name;
        editedItem.mcpBoardId = selectedMCPBoard.id;
        editedItem.mcpBoardName = selectedMCPBoard.name;
        editedItem.functionalLocationId = selectedFunctionalLocation.id;
        editedItem.functionalLocationName = selectedFunctionalLocation.name;
        editedItem.panel = panel;
        editedItem.link = selectedMineInformation.name + " " + selectedSiteInformation.name + " " + selectedMCPBoard.name + " " + panel + " " + selectedFunctionalLocation.name;
      }
      else
      {
      let existItem = this.selectedList.filter(item => item.mineInformationId === this.mcp.get('mineInformationId').value
      && item.siteInformationId === this.mcp.get('siteInformationId').value
      && item.mcpBoardId === this.mcp.get('mcpBoardId').value
      && item.panel === this.mcp.get('panel').value
      && item.functionalLocationId === this.mcp.get('functionalLocationId').value
      );

      if (existItem.length > 0) {
          return this.toastr.info("Unsuccessful attempt! Selected item already added");
      }

      let mcpLink = new MCPLink();
      mcpLink.id = (Math.random() * 1000).toString();
      mcpLink.mineInformationId = selectedMineInformation.id;
      mcpLink.mineInformationName = selectedMineInformation.name;
      mcpLink.siteInformationId = selectedSiteInformation.id;
      mcpLink.siteInformationName = selectedSiteInformation.name;
      mcpLink.mcpBoardId = selectedMCPBoard.id;
      mcpLink.mcpBoardName = selectedMCPBoard.name;
      mcpLink.functionalLocationId = selectedFunctionalLocation.id;
      mcpLink.functionalLocationName = selectedFunctionalLocation.name;
      mcpLink.panel = panel;
      mcpLink.link = selectedMineInformation.name + " " + selectedSiteInformation.name + " " + selectedMCPBoard.name + " " + panel + " " + selectedFunctionalLocation.name; 

      this.selectedList.push(mcpLink);
    }
  }
  }

}
