import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RestService } from '../../../services/rest.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-functional-location-create',
  templateUrl: './functional-location-create.component.html',
  styleUrls: ['./functional-location-create.component.css'],
})
export class FunctionalLocationCreateComponent implements OnInit {
  // selectedFiles?: File;
  // selectedFileNames: string = '';

  // previews: string = '';
  // imageSrc: string | ArrayBuffer = '';

  location!: FormGroup;
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
      rest.fetchFunctionalLocationById(this.id).subscribe((response) => {
        // this.imageSrc = response.image
        this.location.setValue({
          id: response.id,
          name: response.name,
          description: response.description,
          // image: response.image,
        });
      });
    }
  }

  // readURL(event: any): void {
  //   if (event.target.files && event.target.files[0]) {
  //     const file = event.target.files[0];

  //     const reader = new FileReader();
  //     reader.onload = (e) => (this.imageSrc = reader.result);
  //     reader.readAsDataURL(file);
  //   }
  // }

  ngOnInit(): void {
    this.location = this.fb.group({
      id: '',
      name: ['', Validators.required],
      description: ['', Validators.required],
      //image: '',
    });
  }

  onSubmit() {
    if (this.location.invalid) {
      return;
    }

    var data = this.location.value;

    if (this.id === undefined) {
      data.id = '';
      //data.image = this.imageSrc;
      this.rest.createFunctionalLocation(data).subscribe(
        () => {
          this.toastr.success(
            'Functional Location Created Successfully',
            'Functional Location'
          );
          setTimeout(() => {
            this.router.navigate(['/pages/functional-location/list']);
          }, 2000);
        },
        (error) => {
          this.toastr.error(error.error, 'Functional Location');
        }
      );
    } else {
      //data.image = this.imageSrc;
      this.rest.updateFunctionalLocation(data).subscribe(
        () => {
          this.toastr.success('Functional Location Updated Successfully', 'Functional Location');
          setTimeout(() => {
            this.router.navigate(['/pages/functional-location/list']);
          }, 2000);
        },
        (error) => {
          this.toastr.error(error.error, 'Functional Location');
        }
      );
    }
  }
}
