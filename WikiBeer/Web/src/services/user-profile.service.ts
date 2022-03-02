import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Beer } from 'src/models/beer';
import { Country } from 'src/models/country';
import { UserConnectionInfos } from 'src/models/users/user-connection-infos';
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
    return this._httpClient.get<UserProfile>(this.baseUrl + this.userController + id)
    .pipe(map( (data: UserProfile) => {return data;}));
  }

  getFavoriteBeers(id: string): Observable<Beer[]>{
    return this._httpClient.get<Beer[]>(this.baseUrl + this.userController + id +"/favoriteBeers" );
  }

  postUserProfile(profile: UserProfile): Observable<UserProfile>{
    let jsonProfile = this.getJsonProfile(profile);
    const body = JSON.stringify(jsonProfile);
    console.log(body)
    return this._httpClient.post<UserProfile>(this.baseUrl + this.userController, body, this.headers)
  }

  putUserProfile(profile: UserProfile): Observable<UserProfile>{
    let jsonProfile = this.getJsonProfile(profile);
    const body = JSON.stringify(jsonProfile);
    console.log(body)
    return this._httpClient.put<UserProfile>(this.baseUrl + this.userController + profile.id, body, this.headers)
  }

  getJsonProfile(profile: UserProfile){
    let jsonProfile = {
      'id' : profile.id,
      'nickname' : profile.nickname,
      'isCertified' : profile.isCertified,
      'birthDate' : profile.birthDate,
      'country': this.getJsonCountry(profile.country),
      'favoriteBeerIds' : profile.favoriteBeerIds,
      'connectionInfos' : this.getJsonConnectionInfos(profile.connectionInfos)
    }
    return jsonProfile;
  }

  getJsonConnectionInfos(infos: UserConnectionInfos){
    let jsonInfos = {
      'id' : infos.id,
      'email' : infos.email,
      'isVerified' : infos.isVerified
    }
    return jsonInfos;
  }

  getJsonCountry(country : Country){
    if (country)
    {
      let jsonCountry = {
        'id' : country.id,
        'name' : country.name
      }
      return jsonCountry;
    }
    else
    {
      return null;
    }

  }
}
