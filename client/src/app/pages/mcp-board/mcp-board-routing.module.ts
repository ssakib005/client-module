import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NgxMCPBoardComponent } from './mcp-board.component';
import { MCPBoardListComponent } from './mcp-board-list/mcp-board-list.component';
import { MCPBoardCreateComponent } from './mcp-board-create/mcp-board-create.component';


const froutes: Routes = [{
  path: '',
  component: NgxMCPBoardComponent,
  children: [
    {
      path: 'list',
      component: MCPBoardListComponent,
    },
    {
      path: 'create',
      component: MCPBoardCreateComponent,
    },
    {
      path: 'edit/:id',
      component: MCPBoardCreateComponent,
    },
    {
      path: '',
      redirectTo: 'list',
      pathMatch: 'full',
    }
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(froutes)],
  exports: [RouterModule],
})
export class NgxMCPBoardRoutingModule { }

export const mcpBoardRoutedComponents = [
  NgxMCPBoardComponent,
  MCPBoardListComponent,
  MCPBoardCreateComponent
];
