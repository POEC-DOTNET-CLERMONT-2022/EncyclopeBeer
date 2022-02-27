import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Beer } from 'src/models/beer';
import { UserProfile } from 'src/models/users/user-profile';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {

  baseUrl: string ='https://localhost:7160/api/';
  userController: string = 'users/';
  headers: {headers: HttpHeaders} = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json-patch+json',
    })
  };
  /* httpClient par injection de dépendance */
  private _httpClient: HttpClient;

  constructor(httpClient: HttpClient) {
    this._httpClient = httpClient;
  }

  getUserProfileBySub(connectionId: string): Observable<UserProfile> {
    let tt = this._httpClient.get<UserProfile>(this.baseUrl + this.userController + "connection/" + connectionId);
    console.log(this.baseUrl + this.userController + "connection/" + connectionId)
    return tt;
  }

  getUserProfileById(id: string): Observable<UserProfile> {
/*     let id2: string = "a6c7d1fc-6d4a-48fa-d60b-08d9f8af6db5"; */
    let id2 = id;
    return this._httpClient.get<UserProfile>(this.baseUrl + this.userController + id2)
    .pipe(map( (data: UserProfile) => {return data;}));
  }

  getFavoriteBeer(id: string): Observable<Beer>{
    return this._httpClient.get<Beer>(this.baseUrl + this.userController + id +"/favoritesBeers" );
  }

  postUserProfile(profile: UserProfile): Observable<UserProfile>{
    const body = JSON.stringify(UserProfile)
    return this._httpClient.post<UserProfile>(this.baseUrl + this.userController, body, this.headers)
  }

}
