import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient) { }
  private baseUrl: string ='https://localhost:7080/api';

  login(payload: any) : Observable<any>{
    const url = this.baseUrl + '/Users/Login';
    return this.http.post(url, payload).pipe(map((response: any) => response));
  }
  
  forgotPassword(payload: any) : Observable<any>{
    const url = this.baseUrl + '/Users/ForgotPassword';
    return this.http.post(url, payload).pipe(map((response: any) => response));
  }
}
