import { Component, OnInit } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';
import { SmartTableData } from '../../../@core/data/smart-table';
import { RestService } from '../../../services/rest.service';
import { map } from 'rxjs/operators';
import { User } from '../../../model/user.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
})
export class UserListComponent implements OnInit {
  userList: User[];

  constructor(private rest: RestService, private toastr: ToastrService) {}
  ngOnInit(): void {
    this.fetchUserList();
  }

  fetchUserList(): void {
    this.rest
      .fetchUserList()
      .subscribe((response) => (this.userList = response));
  }

  deleteUser(userId): void {
    this.rest.deleteUserById(userId).subscribe(
      (response) => {
        if (response) {
          this.toastr.success('User Deleted Successfully', 'User');
          this.fetchUserList();
        } else {
          this.toastr.error('User not found', 'User');
        }
      },
      (error) => {
        this.toastr.error('Something wrong! Please try again later', 'User');
      }
    );
  }
}
