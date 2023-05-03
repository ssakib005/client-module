import { NgModule } from '@angular/core';
import { NbButtonModule, NbCardModule, NbIconModule, NbInputModule, NbUserModule } from '@nebular/theme';
import { ReactiveFormsModule } from '@angular/forms';
import { ThemeModule } from '../../@theme/theme.module';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { NgxMCPBoardRoutingModule, mcpBoardRoutedComponents } from './mcp-board-routing.module';

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
    NgxMCPBoardRoutingModule
  ],
  declarations: [
    ...mcpBoardRoutedComponents
  ],
})
export class NgxMCPBoardModule { }
