import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { User } from '../model/user.model';
import { FunctionalLocation } from '../model/functional.model';
import {SiteInformation} from '../model/siteInformation.model';
import { MineInformation } from '../model/mineInformation.model';
import { MCPBoard } from '../model/mcpBoard.model';
import { MCPLink } from '../model/mcpLink.model';


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

  fetchFunctionalLocationList(): Observable<FunctionalLocation[]> {
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

  fetchFunctionalLocationListBySiteInformationId(id: string): Observable<FunctionalLocation[]> {
    const url = environment.apiUrl + '/FunctionalLocation/GetListBySiteInformationId' + id;
    return this.http.get(url).pipe(map((response: any) => response.data));
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

  fetchSiteInformationList(): Observable<SiteInformation[]> {
    const url = environment.apiUrl + '/SiteInformation/GetAll';
    return this.http.get(url).pipe(map((response: any) => response.data));
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

  fetchSiteInformationListByMineInformationId(id: string): Observable<SiteInformation[]> {
    const url = environment.apiUrl + '/SiteInformation/GetListByMineInformationId' + id;
    return this.http.get(url).pipe(map((response: any) => response.data));
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

  // MCP Board
  createMCPBoard(payload: any): Observable<any> {
    const url = environment.apiUrl + '/MCPBoard/Create';
    return this.http.post(url, payload).pipe(map((response: any) => response));
  }


  updateMCPBoard(payload: any): Observable<any> {
    const url = environment.apiUrl + '/MCPBoard/Update';
    return this.http.put(url, payload).pipe(map((response: any) => response));
  }


  fetchMCPBoardList(): Observable<MCPBoard[]> {
    const url = environment.apiUrl + '/MCPBoard/GetAll';
    return this.http.get(url).pipe(map((response: any) => response.data));
  }

  fetchMCPBoardById(id: string): Observable<MCPBoard> {
    const url = environment.apiUrl + '/MCPBoard/GetById/' + id;
    return this.http.get(url).pipe(map((response: any) => response.data as MCPBoard));
  }

  deleteMCPBoardById(id: string): Observable<boolean> {
    const url = environment.apiUrl + '/MCPBoard/Delete/' + id;
    return this.http
      .delete(url)
      .pipe(map((response: any) => response as boolean));
  }

  // MCP Link
  createMCPLink(payload: any): Observable<any> {
    const url = environment.apiUrl + '/MCPLink/Create';
    return this.http.post(url, payload).pipe(map((response: any) => response));
  }


  updateMCPLink(payload: any): Observable<any> {
    const url = environment.apiUrl + '/MCPLink/Update';
    return this.http.put(url, payload).pipe(map((response: any) => response));
  }


  fetchMCPLinkList(): Observable<MCPLink[]> {
    const url = environment.apiUrl + '/MCPLink/GetAll';
    return this.http.get(url).pipe(map((response: any) => response.data));
  }

  fetchMCPLinkById(id: string): Observable<MCPLink> {
    const url = environment.apiUrl + '/MCPLink/GetById/' + id;
    return this.http.get(url).pipe(map((response: any) => response.data as MCPLink));
  }

  deleteMCPLinkById(id: string): Observable<boolean> {
    const url = environment.apiUrl + '/MCPLink/Delete/' + id;
    return this.http
      .delete(url)
      .pipe(map((response: any) => response as boolean));
  }
}
