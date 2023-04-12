import { NgModule } from '@angular/core';
import { NbButtonModule, NbCardModule, NbIconModule, NbInputModule } from '@nebular/theme';
import { ReactiveFormsModule } from '@angular/forms';
import { ThemeModule } from '../../@theme/theme.module';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { NgxFunctionalLocationRoutingModule, functionalRoutedComponents } from './functional-location-routing.module';

@NgModule({
  imports: [
    NbCardModule,
    NbIconModule,
    NbInputModule,
    NbButtonModule,
    ThemeModule,
    NgxFunctionalLocationRoutingModule,
    Ng2SmartTableModule,
    ReactiveFormsModule
  ],
  declarations: [
    ...functionalRoutedComponents
  ],
})
export class NgxFunctionalLocationModule { }
