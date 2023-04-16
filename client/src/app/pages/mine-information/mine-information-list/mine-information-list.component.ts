import { Component, OnInit } from '@angular/core';
import { RestService } from '../../../services/rest.service';
import { ToastrService } from 'ngx-toastr';
import { MineInformation } from '../../../model/mineInformation.model';

@Component({
  selector: 'app-mine-information-list',
  templateUrl: './mine-information-list.component.html',
  styleUrls: ['./mine-information-list.component.css'],
})
export class MineInformationListComponent implements OnInit {
  list: MineInformation[];

  constructor(private rest: RestService, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.fetchMineInformationList();
  }

  fetchMineInformationList(): void {
    this.rest
      .fetchMineInformationList()
      .subscribe((response) => (this.list = response.data));
  }

  deleteMineInformation(siteInformationId): void {
    this.rest.deleteMineInformationById(siteInformationId).subscribe(
      (response) => {
        if (response) {
          this.toastr.success('Mine Information Deleted Successfully', 'Mine Information');
          this.fetchMineInformationList();
        } else {
          this.toastr.error('Mine Information not found', 'Mine Information');
        }
      },
      (error) => {
        this.toastr.error('Something wrong! Please try again later', 'Mine Information');
      }
    );
  }
}
