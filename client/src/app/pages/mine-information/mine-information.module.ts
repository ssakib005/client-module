import { NgModule } from '@angular/core';
import { NbAutocompleteModule, NbButtonModule, NbCardModule, NbIconModule, NbInputModule, NbSelectModule, NbUserModule } from '@nebular/theme';
import { ReactiveFormsModule } from '@angular/forms';
import { ThemeModule } from '../../@theme/theme.module';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { NgxMineInformationRoutingModule, mineInformationRoutedComponents } from './mine-information-routing.module';

@NgModule({
  imports: [
    NbCardModule,
    NbIconModule,
    NbInputModule,
    NbButtonModule,
    NbUserModule,
    ThemeModule,
    Ng2SmartTableModule,
    ReactiveFormsModule,
    NgxMineInformationRoutingModule,
    NbSelectModule,
    NbAutocompleteModule
  ],
  declarations: [
    ...mineInformationRoutedComponents
  ],
})
export class NgxMineInformationModule { }
