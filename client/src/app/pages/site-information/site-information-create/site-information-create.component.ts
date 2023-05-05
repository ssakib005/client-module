import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
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
  // selectedFiles?: File;
  // selectedFileNames: string = '';

  // previews: string = '';
  // imageSrc: string | ArrayBuffer = '';

  siteInformation!: FormGroup;
  id: string = undefined;
  isCreate: boolean = true;
  list: FunctionalLocation[] = [];
  selectedIds: String[] = [];
  parentSelector: boolean = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private rest: RestService,
    private toastr: ToastrService,
    private cd: ChangeDetectorRef
  ) {
    this.id = this.route.snapshot.params['id'];

    if (this.id !== undefined) {
      this.isCreate = false;
      rest.fetchSiteInformationById(this.id).subscribe((response) => {
        console.table(this.list);
        //this.imageSrc = response.image;
        this.siteInformation.setValue({
          id: response.id,
          name: response.name,
          description: response.description,
          //image: response.image,
          functionalLocationIds: response.functionalLocationList,
        });
        this.selectedIds = response.functionalLocationList;
        this.fetchFunctionalLocationList();
      });
    } else {
      this.fetchFunctionalLocationList();
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
    this.siteInformation = this.fb.group({
      id: '',
      name: ['', Validators.required],
      description: ['', Validators.required],
      functionalLocationIds: [],
      //image: '',
    });
  }

  async fetchFunctionalLocationList(): Promise<void> {
    const response = await this.rest.fetchFunctionalList().toPromise();

    const dataList = response;
    console.log(dataList)
    for(let item of dataList){
      const isContains = this.selectedIds.includes(item.id);
      console.log(isContains);
      if (isContains) {
        item.isChecked = true;
      } else {
        item.isChecked = false;
      }
      this.list.push(item)
    }
    this.cd.detectChanges()
  }
  onSubmit() {
    if (this.siteInformation.invalid) {
      return;
    }
    var data = this.siteInformation.value;
    if (this.id === undefined) {
      data.id = '';
      //data.image = this.imageSrc;
      data.functionalLocationIds = this.list
        .filter(function (obj) {
          return obj.isChecked;
        })
        .map((a) => a.id);
      if (data.functionalLocationIds.length < 1) {
        this.toastr.error(
          'Functional Location is required',
          'Functional Location'
        );
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
      //data.image = this.imageSrc;
      data.functionalLocationIds = this.list
        .filter(function (obj) {
          return obj.isChecked;
        })
        .map((a) => a.id);
      if (data.functionalLocationIds.length < 1) {
        this.toastr.error(
          'Functional Location is required',
          'Functional Location'
        );
        return;
      }
      this.rest.updateSiteInformation(data).subscribe(
        () => {
          this.toastr.success(
            'Site Information Updated Successfully',
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
    }
  }

  updateItemChecked(item: FunctionalLocation , e: any){
    const updatedItem = { ...item, isChecked: e.target.checked };
    const index = this.list.findIndex(i => i.id === item.id);
    console.log(updatedItem.isChecked)
    this.list[index] = updatedItem;
  }

  checkAllCheckBox(ev: any) {
    this.list.forEach((x) => (x.isChecked = ev.target.checked));
  }

  isAllCheckBoxChecked() {
    return this.list.every((p) => p.isChecked);
  }
}
