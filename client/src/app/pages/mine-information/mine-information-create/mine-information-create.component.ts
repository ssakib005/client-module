import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RestService } from '../../../services/rest.service';
import { Observable } from 'rxjs';
import { SiteInformation } from '../../../model/siteInformation.model';

@Component({
  selector: 'app-mine-information-create',
  templateUrl: './mine-information-create.component.html',
  styleUrls: ['./mine-information-create.component.css'],
})
export class MineInformationCreateComponent implements OnInit {
  // selectedFiles?: File;
  // selectedFileNames: string = '';

  // previews: string = '';
  // imageSrc: string | ArrayBuffer = '';

  mineInformation!: FormGroup;
  id: string = undefined;
  isCreate: boolean = true;
  list: SiteInformation[]=[];
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
      rest.fetchMineInformationById(this.id).subscribe((response) => {
        //this.imageSrc = response.image;
        this.mineInformation.setValue({
          id: response.id,
          name: response.name,
          description: response.description,
          //image: response.image,
          siteInformationIds: response.siteInformationList,
        });
        this.selectedIds = response.siteInformationList;
        this.fetchSiteInformationList();
      });
    } else {
      this.fetchSiteInformationList();
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
    
    this.mineInformation = this.fb.group({
      id: '',
      name: ['', Validators.required],
      description: ['', Validators.required],
      siteInformationIds: [''],
      //image: '',
    });
  }

  async fetchSiteInformationList(): Promise<void> {
    const response = await this.rest.fetchSiteInformationList().toPromise();

    const dataList = response;
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
    if (this.mineInformation.invalid) {
      return;
    }
    var data = this.mineInformation.value;

    if (this.id === undefined) {
      data.id = '';
      //data.image = this.imageSrc;
      data.siteInformationIds = this.list
        .filter(function (obj) {
          return obj.isChecked;
        })
        .map((a) => a.id);
      if (data.siteInformationIds.length < 1) {
        this.toastr.error(
          'Site Informaion is required',
          'Site Informaion'
        );
        return;
      }
      this.rest.createMineInformation(data).subscribe(
        () => {
          this.toastr.success(
            'Mine Information Created Successfully',
            'Mine Information'
          );
          setTimeout(() => {
            this.router.navigate(['/pages/mine-information/list']);
          }, 2000);
        },
        (error) => {
          this.toastr.error(error.error, 'Mine Information');
        }
      );
    } else {
      //data.image = this.imageSrc;
      data.siteInformationIds = this.list
        .filter(function (obj) {
          return obj.isChecked;
        })
        .map((a) => a.id);
      if (data.siteInformationIds.length < 1) {
        this.toastr.error(
          'Site Informaion is required',
          'Site Informaion'
        );
        return;
      }
      this.rest.updateMineInformation(data).subscribe(
        () => {
          this.toastr.success('Mine Information Updated Successfully', 'Mine Information');
          setTimeout(() => {
            this.router.navigate(['/pages/mine-information/list']);
          }, 2000);
        },
        (error) => {
          this.toastr.error(error.error, 'Mine Information');
        }
      );
    }
  }

  updateItemChecked(item: SiteInformation , e: any){
    const updatedItem = { ...item, isChecked: e.target.checked };
    const index = this.list.findIndex(i => i.id === item.id);
    this.list[index] = updatedItem;
  }

  checkAllCheckBox(ev: any) {
    this.list.forEach((x) => (x.isChecked = ev.target.checked));
  }

  isAllCheckBoxChecked() {
    return this.list.every((p) => p.isChecked);
  }
}
