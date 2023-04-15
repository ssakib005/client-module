import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RestService } from '../../../services/rest.service';
import { Observable } from 'rxjs';
import { FunctionalLocation } from '../../../model/functional.model';

@Component({
  selector: 'app-site-information-create',
  templateUrl: './site-information-create.component.html',
  styleUrls: ['./site-information-create.component.css'],
})
export class SiteInformationCreateComponent implements OnInit {
  selectedFiles?: File;
  selectedFileNames: string = '';

  previews: string = '';
  imageSrc: string | ArrayBuffer = '';

  siteInformation!: FormGroup;
  id: string = undefined;
  isCreate: boolean = true;
  list: FunctionalLocation[];

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
      rest.fetchSiteInformationById(this.id).subscribe((response) => {
        this.imageSrc = response.image
        this.siteInformation.setValue({
          id: response.id,
          name: response.name,
          description: response.description,
          image: response.image,
          functionalLocationId: response.functionalLocationId
        });
      });
    }
  }

  readURL(event: any): void {
    if (event.target.files && event.target.files[0]) {
      const file = event.target.files[0];

      const reader = new FileReader();
      reader.onload = (e) => (this.imageSrc = reader.result);
      reader.readAsDataURL(file);
    }
  }

  ngOnInit(): void {
    this.fetchFunctionalLocationList();
    this.siteInformation = this.fb.group({
      id: '',
      name: ['', Validators.required],
      description: ['', Validators.required],
      functionalLocationId: ['',Validators.required],
      image: '',
    });
  }
  fetchFunctionalLocationList(): void {
    this.rest
      .fetchFunctionalList()
      .subscribe((response) => (this.list = response.data));
  }
  onSubmit() {
    if (this.siteInformation.invalid) {
      return;
    }

    var data = this.siteInformation.value;

    if (this.id === undefined) {
      data.id = '';
      data.image = this.imageSrc;
      this.rest.createSiteInformation(data).subscribe(
        () => {
          this.toastr.success(
            'Site Information Created Successfully',
            'Site Information'
          );
          setTimeout(() => {
            this.router.navigate(['/pages/site-information/list']);
          }, 2000);
        },
        (error) => {
          this.toastr.error(error.error, 'Site Information');
        }
      );
    } else {
      data.image = this.imageSrc;
      this.rest.updateSiteInformation(data).subscribe(
        () => {
          this.toastr.success('Site Information Updated Successfully', 'Site Information');
          setTimeout(() => {
            this.router.navigate(['/pages/site-information/list']);
          }, 2000);
        },
        (error) => {
          this.toastr.error(error.error, 'Site Information');
        }
      );
    }
  }
}
