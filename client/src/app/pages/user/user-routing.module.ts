import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserListComponent } from './user-list/user-list.component';
import { UserCreateComponent } from './user-create/user-create.component';
import { NgxUserComponent } from './user.component';


const routes: Routes = [{
  path: '',
  component: NgxUserComponent,
  children: [
    {
      path: 'user-list',
      component: UserListComponent,
    },
    {
      path: 'user-create',
      component: UserCreateComponent,
    },
    {
      path: 'user-edit/:id',
      component: UserCreateComponent,
    },
    {
      path: '',
      redirectTo: 'user-list',
      pathMatch: 'full',
    }
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class NgxUserRoutingModule { }

export const routedComponents = [
  NgxUserComponent,
  UserListComponent,
  UserCreateComponent,
];
