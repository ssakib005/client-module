import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NgxSiteInformationComponent } from './site-information.component';
import { SiteInformationListComponent } from './site-information-list/site-information-list.component';
import { SiteInformationCreateComponent } from './site-information-create/site-information-create.component';
import { UploadImagesComponent } from '../../components/image_upload/upload-images.component';


const froutes: Routes = [{
  path: '',
  component: NgxSiteInformationComponent,
  children: [
    {
      path: 'list',
      component: SiteInformationListComponent,
    },
    {
      path: 'create',
      component: SiteInformationCreateComponent,
    },
    {
      path: 'edit/:id',
      component: SiteInformationCreateComponent,
    },
    {
      path: '',
      redirectTo: 'list',
      pathMatch: 'full',
    }
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(froutes)],
  exports: [RouterModule],
})
export class NgxSiteInformationRoutingModule { }

export const siteInformationRoutedComponents = [
  NgxSiteInformationComponent,
  SiteInformationListComponent,
  SiteInformationCreateComponent
];
