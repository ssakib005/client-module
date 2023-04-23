import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { User } from '../model/user.model';
import { FunctionalLocation } from '../model/functional.model';
import {SiteInformation} from '../model/siteInformation.model';
import { MineInformation } from '../model/mineInformation.model';


@Injectable({
  providedIn: 'root',
})
export class RestService {
  constructor(private http: HttpClient) {}

  //User
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
    const url = environment.apiUrl + '/Account/User/' + id;
    return this.http.get(url).pipe(map((response: any) => response as User));
  }

  deleteUserById(id: string): Observable<boolean> {
    const url = environment.apiUrl + '/Account/User-delete/' + id;
    return this.http
      .delete(url)
      .pipe(map((response: any) => response as boolean));
  }

  // Functional Location
  createFunctionalLocation(payload: any): Observable<any> {
    const url = environment.apiUrl + '/FunctionalLocation/Create';
    return this.http.post(url, payload).pipe(map((response: any) => response));
  }


  updateFunctionalLocation(payload: any): Observable<any> {
    const url = environment.apiUrl + '/FunctionalLocation/Update';
    return this.http.put(url, payload).pipe(map((response: any) => response));
  }


  fetchFunctionalList(): Observable<FunctionalLocation[]> {
    const url = environment.apiUrl + '/FunctionalLocation/GetAll';
    return this.http.get(url).pipe(map((response: any) => response.data));
  }

  fetchFunctionalLocationById(id: string): Observable<FunctionalLocation> {
    const url = environment.apiUrl + '/FunctionalLocation/GetById/' + id;
    return this.http.get(url).pipe(map((response: any) => response.data as FunctionalLocation));
  }

  deleteFunctionalLocationById(id: string): Observable<boolean> {
    const url = environment.apiUrl + '/FunctionalLocation/Delete/' + id;
    return this.http
      .delete(url)
      .pipe(map((response: any) => response as boolean));
  }

  // Site Information
  createSiteInformation(payload: any): Observable<any> {
    const url = environment.apiUrl + '/SiteInformation/Create';
    return this.http.post(url, payload).pipe(map((response: any) => response));
  }


  updateSiteInformation(payload: any): Observable<any> {
    const url = environment.apiUrl + '/SiteInformation/Update';
    return this.http.put(url, payload).pipe(map((response: any) => response));
  }


  fetchSiteInformationList(): Observable<any> {
    const url = environment.apiUrl + '/SiteInformation/GetAll';
    return this.http.get(url).pipe(map((response: any) => response));
  }

  fetchSiteInformationById(id: string): Observable<SiteInformation> {
    const url = environment.apiUrl + '/SiteInformation/GetById/' + id;
    return this.http.get(url).pipe(
      map((response: any) => response.data as SiteInformation));
  }

  deleteSiteInformationById(id: string): Observable<boolean> {
    const url = environment.apiUrl + '/SiteInformation/Delete/' + id;
    return this.http
      .delete(url)
      .pipe(map((response: any) => response as boolean));
  }


  // Mine Information
  createMineInformation(payload: any): Observable<any> {
    const url = environment.apiUrl + '/MineInformation/Create';
    return this.http.post(url, payload).pipe(map((response: any) => response));
  }


  updateMineInformation(payload: any): Observable<any> {
    const url = environment.apiUrl + '/MineInformation/Update';
    return this.http.put(url, payload).pipe(map((response: any) => response));
  }


  fetchMineInformationList(): Observable<any> {
    const url = environment.apiUrl + '/MineInformation/GetAll';
    return this.http.get(url).pipe(map((response: any) => response));
  }

  fetchMineInformationById(id: string): Observable<MineInformation> {
    const url = environment.apiUrl + '/MineInformation/GetById/' + id;
    return this.http.get(url).pipe(
      map((response: any) => response.data as MineInformation));
  }

  deleteMineInformationById(id: string): Observable<boolean> {
    const url = environment.apiUrl + '/MineInformation/Delete/' + id;
    return this.http
      .delete(url)
      .pipe(map((response: any) => response as boolean));
  }
}
