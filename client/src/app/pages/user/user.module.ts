import { NgModule } from '@angular/core';
import { NbButtonModule, NbCardModule, NbIconModule, NbInputModule } from '@nebular/theme';
import { Ng2SmartTableModule } from 'ng2-smart-table';

import { ThemeModule } from '../../@theme/theme.module';
import { NgxUserRoutingModule, routedComponents } from './user-routing.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    NbCardModule,
    NbIconModule,
    NbInputModule,
    NbButtonModule,
    ThemeModule,
    NgxUserRoutingModule,
    Ng2SmartTableModule,
    ReactiveFormsModule
  ],
  declarations: [
    ...routedComponents
  ],
})
export class NgxUserModule { }
