import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NgxFunctionalLocationComponent } from './functional-location.component';
import { FunctionalLocationListComponent } from './functional-location-list/functional-location-list.component';
import { routes } from '@nebular/auth';
import { FunctionalLocationCreateComponent } from './functional-location-create/functional-location-create.component';


const froutes: Routes = [{
  path: '',
  component: NgxFunctionalLocationComponent,
  children: [
    {
      path: 'functional-lication-list',
      component: FunctionalLocationListComponent,
    },
    {
      path: 'functional-lication-create',
      component: FunctionalLocationCreateComponent,
    },
    {
      path: 'functional-lication-edit/:id',
      component: FunctionalLocationCreateComponent,
    },
    {
      path: '',
      redirectTo: 'functional-lication-list',
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
