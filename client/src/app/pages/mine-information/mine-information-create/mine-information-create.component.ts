import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
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
  selectedFiles?: File;
  selectedFileNames: string = '';

  previews: string = '';
  imageSrc: string | ArrayBuffer = '';

  mineInformation!: FormGroup;
  id: string = undefined;
  isCreate: boolean = true;
  list: SiteInformation[];
  selectedList: SiteInformation[] = [];

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
      rest.fetchMineInformationById(this.id).subscribe((response) => {
        this.imageSrc = response.image;
        this.selectedList = response.siteInformationList;
        this.mineInformation.setValue({
          id: response.id,
          name: response.name,
          description: response.description,
          image: response.image,
          siteInformationId : ""
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
    this.fetchSiteInformationList();
    this.mineInformation = this.fb.group({
      id: '',
      name: ['', Validators.required],
      description: ['', Validators.required],
      siteInformationId: [''],
      image: '',
    });
  }
  fetchSiteInformationList(): void {
    this.rest
      .fetchSiteInformationList()
      .subscribe((response) => (this.list = response.data));
  }
  onSubmit() {
    if (this.mineInformation.invalid) {
      return;
    }
    if(this.selectedList.length < 1){
      this.toastr.error("Site Informaion is required", 'Mine Information');
      return;
    }

    var data = this.mineInformation.value;

    if (this.id === undefined) {
      data.id = '';
      data.image = this.imageSrc;
      data.SiteInformationIds = this.selectedList.map(a => a.id);
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
      data.image = this.imageSrc;
      data.SiteInformationIds = this.selectedList.map(a => a.id);
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
  addMineInformation() {
    if (this.mineInformation.get('siteInformationId').value == null) {
      return;
    }
    else {
      let selectedSiteInformation = this.list?.find(x => x.id === this.mineInformation.get('siteInformationId').value);
      
      let existItem = this.selectedList.filter(item => item.id === this.mineInformation.get('siteInformationId').value);
      if (existItem.length > 0) {
          return this.toastr.info("Unsuccessful attempt! Selected item already added");
      }

      let siteInformation = new SiteInformation();
      siteInformation.id = selectedSiteInformation.id;
      siteInformation.name = selectedSiteInformation.name;

      this.selectedList.push(siteInformation);

      this.mineInformation.get('siteInformationId').setValue("");
    }
  }
  deleteSiteInformation(siteInformation: SiteInformation) {
    const index = this.selectedList.indexOf(siteInformation);
		if (index >= 0) {
			this.selectedList.splice(index, 1);
		}
  }
}
