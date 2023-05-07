import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NgxMCPLinkComponent } from './mcp-link.component';
import { MCPLinkListComponent } from './mcp-link-list/mcp-link-list.component';
import { MCPLinkCreateComponent } from './mcp-link-create/mcp-link-create.component';


const froutes: Routes = [{
  path: '',
  component: NgxMCPLinkComponent,
  children: [
    {
      path: 'list',
      component: MCPLinkListComponent,
    },
    {
      path: 'create',
      component: MCPLinkCreateComponent,
    },
    {
      path: 'edit/:id',
      component: MCPLinkCreateComponent,
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
export class NgxMCPLinkRoutingModule { }

export const mcpLinkRoutedComponents = [
  NgxMCPLinkComponent,
  MCPLinkListComponent,
  MCPLinkCreateComponent
];
