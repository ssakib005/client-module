import { NgModule } from '@angular/core';
import { NbButtonModule, NbCardModule, NbIconModule, NbInputModule, NbSelectModule, NbUserModule } from '@nebular/theme';
import { ReactiveFormsModule } from '@angular/forms';
import { ThemeModule } from '../../@theme/theme.module';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { NgxMCPLinkRoutingModule, mcpLinkRoutedComponents } from './mcp-link-routing.module';

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
    NgxMCPLinkRoutingModule,
    NbSelectModule
  ],
  declarations: [
    ...mcpLinkRoutedComponents
  ],
})
export class NgxMCPLinkModule { }
