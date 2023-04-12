import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NbTokenService } from '@nebular/auth';

@Component({
  selector: 'app-logout',
  template: '',
})
export class LogoutComponent implements OnInit {
  constructor(private nbTokenService: NbTokenService, private router: Router) {}

  ngOnInit(): void {
    this.logout()
  }

  logout(): void{
    this.nbTokenService.clear();
    this.router.navigate(['/auth']);
  }
}
