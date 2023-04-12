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
export class AuthGuard implements CanActivate {
  constructor(private authService: NbAuthService, private router: Router) {}

  canActivate() {
    return this.authService.isAuthenticated().pipe(
      map((isLogin) => {
        console.log("Is Login", isLogin)
        if (!isLogin) {
          this.router.navigate(['/auth']);
          return false;
        } else {
          return true;
        }
      })
    );
  }
}
