import { Component, OnInit } from '@angular/core';
import { RestService } from '../../../services/rest.service';
import { ToastrService } from 'ngx-toastr';
import { FunctionalLocation } from '../../../model/functional.model';

@Component({
  selector: 'app-functional-location-list',
  templateUrl: './functional-location-list.component.html',
  styleUrls: ['./functional-location-list.component.css'],
})
export class FunctionalLocationListComponent implements OnInit {
  list: FunctionalLocation[];

  constructor(private rest: RestService, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.fetchUserList();
  }

  fetchUserList(): void {
    this.rest
      .fetchFunctionalList()
      .subscribe((response) => (this.list = response.data));
  }

  deleteUser(userId): void {
    this.rest.deleteFunctionalLocationById(userId).subscribe(
      (response) => {
        if (response) {
          this.toastr.success('Functional Location Deleted Successfully', 'Functional Location');
          this.fetchUserList();
        } else {
          this.toastr.error('Functional Location not found', 'User');
        }
      },
      (error) => {
        this.toastr.error('Something wrong! Please try again later', 'Functional Location');
      }
    );
  }
}
