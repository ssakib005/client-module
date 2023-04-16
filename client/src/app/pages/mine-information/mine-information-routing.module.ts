import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NgxMineInformationComponent } from './mine-information.component';
import { MineInformationListComponent } from './mine-information-list/mine-information-list.component';
import { MineInformationCreateComponent } from './mine-information-create/mine-information-create.component';
import { UploadImagesComponent } from '../../components/image_upload/upload-images.component';


const froutes: Routes = [{
  path: '',
  component: NgxMineInformationComponent,
  children: [
    {
      path: 'list',
      component: MineInformationListComponent,
    },
    {
      path: 'create',
      component: MineInformationCreateComponent,
    },
    {
      path: 'edit/:id',
      component: MineInformationCreateComponent,
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
export class NgxMineInformationRoutingModule { }

export const mineInformationRoutedComponents = [
  NgxMineInformationComponent,
  MineInformationListComponent,
  MineInformationCreateComponent
];
