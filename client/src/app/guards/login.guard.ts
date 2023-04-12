import { Injectable } from '@angular/core';
import {
  CanActivate,
  Router,
} from '@angular/router';
import { NbAuthService } from '@nebular/auth';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class LoginGuard implements CanActivate {
  constructor(private authService: NbAuthService, private router: Router) {}


  canActivate() {
    return this.authService.isAuthenticated().pipe(
      map((isLogin) => {
        console.log(isLogin);
        if (isLogin) {
          this.router.navigate(['/pages']);
          return false;
        } else {
          return true;
        }
      })
    );
  }
}
