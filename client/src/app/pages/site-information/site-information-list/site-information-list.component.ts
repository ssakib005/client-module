import { Component, OnInit } from '@angular/core';
import { RestService } from '../../../services/rest.service';
import { ToastrService } from 'ngx-toastr';
import { SiteInformation } from '../../../model/siteInformation.model';

@Component({
  selector: 'app-site-information-list',
  templateUrl: './site-information-list.component.html',
  styleUrls: ['./site-information-list.component.css'],
})
export class SiteInformationListComponent implements OnInit {
  list: SiteInformation[];

  constructor(private rest: RestService, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.fetchSiteInformationList();
  }

  fetchSiteInformationList(): void {
    this.rest
      .fetchSiteInformationList()
      .subscribe((response) => (this.list = response.data));
  }

  deleteSiteInformation(siteInformationId): void {
    this.rest.deleteSiteInformationById(siteInformationId).subscribe(
      (response) => {
        if (response) {
          this.toastr.success('Site Information Deleted Successfully', 'Site Information');
          this.fetchSiteInformationList();
        } else {
          this.toastr.error('Site Information not found', 'Site Information');
        }
      },
      (error) => {
        this.toastr.error('Something wrong! Please try again later', 'Site Information');
      }
    );
  }
}
