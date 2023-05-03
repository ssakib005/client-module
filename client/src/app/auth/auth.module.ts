import { NgModule } from '@angular/core';

import { NgxAuthRoutingModule } from './auth-routing.module';
import { NbAuthJWTToken, NbAuthModule, NbPasswordAuthStrategy } from '@nebular/auth';
import {
  NbCardModule,
  NbFormFieldModule,
  NbIconModule,
  NbInputModule,
  NbLayoutModule,
  NbRadioModule,
} from '@nebular/theme';
import { ThemeModule } from '../@theme/theme.module';
import { AuthComponent } from './auth.component';


const formSetting: any = {
  redirectDelay: 0,
  showMessages: {
    success: true,
  },
};

@NgModule({
  imports: [
    ThemeModule,
    NbCardModule,
    NbInputModule,
    NbIconModule,
    NbLayoutModule,
    NbRadioModule,
    NgxAuthRoutingModule,
    NbFormFieldModule,
    NbAuthModule.forRoot({
      strategies: [
        NbPasswordAuthStrategy.setup({
          name: 'email',
          baseEndpoint: 'https://localhost:7080/api',
          login: {
            alwaysFail: false,
            endpoint: '/Users/Login',
            method: 'post',
            redirect: {
              success: '/pages',
              failure: null,
            },
            defaultErrors: ['Login/Email combination is not correct, please try again.'],
            defaultMessages: ['You have been successfully logged in.'],
          },
          // requestPass: {
          //   alwaysFail: false,
          //   endpoint: '/Users/ForgotPassword',
          //   method: 'post',
          //   redirect: {
          //     success: '/pages',
          //     failure: null,
          //   },
          //   defaultErrors: ['Login/Email combination is not correct, please try again.'],
          //   defaultMessages: ['You have been successfully logged in.'],
          // },
          token: {
            class: NbAuthJWTToken,
            key: 'accessToken', // this parameter tells where to look for the token
          },
          validation: {
            email: {
              required: false, // <-- allow both username and email?????
            }
          }
        }),
      ],
      forms: {
        login: formSetting,
        register: formSetting,
        requestPassword: formSetting,
        resetPassword: formSetting,
        logout: {
          redirectDelay: 0,
        },
      },
    }),
  ],
  declarations: [
    AuthComponent
  ],
})
export class NgxAuthModule {
}
