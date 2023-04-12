import { NgModule } from '@angular/core';
import { NbCardModule, NbMenuModule } from '@nebular/theme';

import { ThemeModule } from '../@theme/theme.module';
import { PagesComponent } from './pages.component';
import { PagesRoutingModule } from './pages-routing.module';
import { NbAuthModule, NbPasswordAuthStrategy } from '@nebular/auth';
import { HomeComponent } from './home/home.component';
import { SiteComponent } from './site/site.component';
import { NotFoundComponent } from './not-found/not-found.component';

@NgModule({
  imports: [
    PagesRoutingModule,
    ThemeModule,
    NbMenuModule,
    NbCardModule,
    NbAuthModule.forRoot({
      strategies: [
        NbPasswordAuthStrategy.setup({
          name: 'email',
          baseEndpoint: 'https://localhost:7080/api',
          logout: {
            endpoint: '',
          },
        }),
      ],
      forms: {
        logout: {
          redirectDelay: 0,
          strategy: 'email',
          redirect: {
            success: '/auth/login', // welcome page path
            failure: null, // stay on the same page
          },
        },
      },
    }),
  ],
  declarations: [
    PagesComponent,
    HomeComponent,
    SiteComponent,
    NotFoundComponent
  ],
})
export class PagesModule {
}
