import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { PagesComponent } from './pages.component';
import { HomeComponent } from './home/home.component';
import { SiteComponent } from './site/site.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { LogoutComponent } from './logout.component';

const routes: Routes = [{
  path: '',
  component: PagesComponent,
  children: [
    {
      path: 'home',
      component: HomeComponent,
    },
    {
      path: 'users',
      loadChildren: () => import('./user/user.module').then((m) => m.NgxUserModule),
    },
    {
      path: 'sites',
      component: SiteComponent,
    },
    {
      path: 'functional-location',
      loadChildren: () => import('./functional-location/functional-location.module').then((m) => m.NgxFunctionalLocationModule),
    },
    {
      path: 'site-information',
      loadChildren: () => import('./site-information/site-information.module').then((m) => m.NgxSiteInformationModule),
    },
    {
      path: 'logout',
      component: LogoutComponent,
    },
    {
      path: '',
      redirectTo: 'home',
      pathMatch: 'full',
    },
    {
      path: '**',
      component: NotFoundComponent,
    },
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {
}
