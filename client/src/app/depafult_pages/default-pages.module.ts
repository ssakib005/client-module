import { NgModule } from '@angular/core';
import { NbMenuModule } from '@nebular/theme';

import { ThemeModule } from '../@theme/theme.module';
import { DefaultPagesComponent } from './default-pages.component';
import { DashboardModule } from './dashboard/dashboard.module';
import { ECommerceModule } from './e-commerce/e-commerce.module';
import { DefaultPagesRoutingModule } from './default-pages-routing.module';
import { MiscellaneousModule } from './miscellaneous/miscellaneous.module';

@NgModule({
  imports: [
    DefaultPagesRoutingModule,
    ThemeModule,
    NbMenuModule,
    DashboardModule,
    ECommerceModule,
    // MiscellaneousModule,
  ],
  declarations: [
    DefaultPagesComponent,
  ],
})
export class DefaultPagesModule {
}
