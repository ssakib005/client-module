import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { User } from '../model/user.model';

@Injectable({
  providedIn: 'root'
})
export class RestService {

  constructor(private http: HttpClient) { }


  createUser(payload: any): Observable<any> {
    const url = environment.apiUrl + '/Account/User';
    return this.http.post(url, payload).pipe(map((response: any) => response));
  }

  updateUser(payload: any): Observable<any> {
    const url = environment.apiUrl + '/Account/User-update';
    return this.http.put(url, payload).pipe(map((response: any) => response));
  }

  fetchUserList(): Observable<any> {
    const url = environment.apiUrl + '/Account/User-list';
    return this.http.get(url).pipe(map((response: any) => response));
  }

  fetchUserById(id: string): Observable<User> {
    const url = environment.apiUrl + '/Account/User/'+id;
    return this.http.get(url).pipe(map((response: any) => response as User));
  }

  deleteUserById(id: string): Observable<boolean> {
    const url = environment.apiUrl + '/Account/User-delete/'+id;
    return this.http.delete(url).pipe(map((response: any) => response as boolean));
  }


}
