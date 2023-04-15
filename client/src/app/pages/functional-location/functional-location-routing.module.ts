import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NgxFunctionalLocationComponent } from './functional-location.component';
import { FunctionalLocationListComponent } from './functional-location-list/functional-location-list.component';
import { FunctionalLocationCreateComponent } from './functional-location-create/functional-location-create.component';
import { UploadImagesComponent } from '../../components/image_upload/upload-images.component';


const froutes: Routes = [{
  path: '',
  component: NgxFunctionalLocationComponent,
  children: [
    {
      path: 'list',
      component: FunctionalLocationListComponent,
    },
    {
      path: 'create',
      component: FunctionalLocationCreateComponent,
    },
    {
      path: 'edit/:id',
      component: FunctionalLocationCreateComponent,
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
export class NgxFunctionalLocationRoutingModule { }

export const functionalRoutedComponents = [
  NgxFunctionalLocationComponent,
  FunctionalLocationListComponent,
  FunctionalLocationCreateComponent
];
