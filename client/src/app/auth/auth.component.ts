import { Component } from '@angular/core';

@Component({
  selector: 'ngx-auth',
  styleUrls: ['auth.component.scss'],
  template: `
    <nb-layout>
      <nb-layout-column>
        <nb-card style="position: fixed;top: 50%;left: 50%;-webkit-transform: translate(-50%, -50%);transform: translate(-50%, -50%); width: 550px; max-width: 720px;">
          <nb-card-body>
            <nb-auth-block style="margin: auto;">
              <router-outlet></router-outlet>
            </nb-auth-block>
          </nb-card-body>
        </nb-card>
      </nb-layout-column>
    </nb-layout>
  `,
})
export class AuthComponent {

}
