import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RestService } from '../../../services/rest.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-user-create',
  templateUrl: './user-create.component.html',
  styleUrls: ['./user-create.component.scss'],
})
export class UserCreateComponent implements OnInit {
  registrationForm!: FormGroup;
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
      rest.fetchUserById(this.id).subscribe((response) => {
        this.registrationForm.setValue({
          id: response.id,
          firstName: response.firstName,
          lastName: response.lastName,
          email: response.email,
          userName: response.username,
          password: response.password,
        });
      });
    }
  }

  ngOnInit(): void {
    this.registrationForm = this.fb.group({
      id: '',
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      userName: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit() {
    if (this.registrationForm.invalid) {
      return;
    }

    var data = this.registrationForm.value;

    if (this.id === undefined) {
      data.id = '';
      this.rest.createUser(data).subscribe(
        () => {
          this.toastr.success('User Created Successfully', 'User');
        },
        (error) => {
          this.toastr.error(error.error, 'User');
        }
      );
    } else {
      this.rest.updateUser(data).subscribe(
        () => {
          this.toastr.success('User Updated Successfully', 'User');
        },
        (error) => {
          this.toastr.error(error.error, 'User');
        }
      );
    }
  }
}
