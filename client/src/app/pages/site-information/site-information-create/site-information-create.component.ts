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
  list: FunctionalLocation[]= [];
  parentSelector: boolean = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private rest: RestService,
    private toastr: ToastrService
    ) 
    {
    this.id = this.route.snapshot.params['id'];
    this.fetchFunctionalLocationList();

    if (this.id !== undefined) {
      debugger;
      this.isCreate = false;
      rest.fetchSiteInformationById(this.id).subscribe((response) => {
        console.table(this.list);
        this.imageSrc = response.image;
        this.siteInformation.setValue({
          id: response.id,
          name: response.name,
          description: response.description,
          image: response.image,
          functionalLocationIds: ''
        });

        this.list.forEach((res) => {
          const listObject = response.functionalLocationList.find((mainObject) => mainObject.id === res.id);
          if (listObject !== undefined) {
            res.isChecked = true;
          }
          else
          res.isChecked=false;
        });
        
        console.table(this.list);
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
    this.siteInformation = this.fb.group({
      id: '',
      name: ['', Validators.required],
      description: ['', Validators.required],
      functionalLocationIds: '',
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
      data.functionalLocationIds = this.list.filter(function(obj) {
        return obj.isChecked;
      }).map(a => a.id);
      if(data.functionalLocationIds.length < 1){
        this.toastr.error("Functional Location is required", 'Functional Location');
        return;
      }
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
      data.functionalLocationIds = this.list.filter(function(obj) {
        return obj.isChecked;
      }).map(a => a.id);
      if(data.functionalLocationIds.length < 1){
        this.toastr.error("Functional Location is required", 'Functional Location');
        return;
      }
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

    checkAllCheckBox(ev: any) {
      this.list.forEach(x => x.isChecked = ev.target.checked)
    }
  
    isAllCheckBoxChecked() {
      return this.list.every(p => p.isChecked);
    }
}
