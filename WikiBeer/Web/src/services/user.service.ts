import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { User } from "src/models/users/user";

import { Injectable, OnInit } from '@angular/core';
import { AuthService } from "@auth0/auth0-angular";
import { UserProfile } from "src/models/users/user-profile";
import { IUserConnectionInfos } from "src/models/users/i-user-connection-infos";

@Injectable({
  providedIn: 'root'
})
export class UserService implements OnInit{
  /* ici la connection string de l'api*/
  baseUrl: string ='https://localhost:7160/api/';
  userController: string = 'users/';
  /* httpClient par injection de dépendance */
  private _httpClient: HttpClient;
  private _auth: AuthService;

/*   public userConnectionInfo : IUserConnectionInfos; */
  private _user : User;

  constructor(httpClient: HttpClient, auth: AuthService)
  {
    this._httpClient = httpClient;
    this._auth = auth;
    this._user = new User();
  }

  ngOnInit(): void {
    /* this._auth.user$.subscribe((u) => this._user.setConnectionInfo(u)); */
  }

  get user(): User {
    return this._user;
  }

  setUserConnectionInfos(): void {
    this._auth.user$.subscribe((u) => this._user.setConnectionInfo(u));
    console.log(this._user);
  }

  getUserProfileBySub(sub: string): Observable<UserProfile> {
    return this._httpClient.get<UserProfile>(this.baseUrl + this.userController + sub)
  }

}
