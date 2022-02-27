import { HttpClient } from "@angular/common/http";
import { BehaviorSubject, map, Observable } from "rxjs";
import { User } from "src/models/users/user";

import { Injectable, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from "@auth0/auth0-angular";
import { UserProfile } from "src/models/users/user-profile";
import { UserConnectionInfos } from "src/models/users/user-connection-infos";
import { UserProfileService } from "./user-profile.service";

@Injectable({
  providedIn: 'root'
})
export class UserService implements OnInit, OnDestroy{
  /* ici la connection string de l'api*/
  baseUrl: string ='https://localhost:7160/api/';
  userController: string = 'users/';
  /* httpClient par injection de dépendance */
  private _httpClient: HttpClient;
  private _userProfileService : UserProfileService;
  private _auth: AuthService;

  private userSource = new BehaviorSubject<User>(new User());
  user = this.userSource.asObservable();

  constructor(httpClient: HttpClient, auth: AuthService, userProfileService : UserProfileService)
  {
    this._httpClient = httpClient;
    this._userProfileService = userProfileService;
    this._auth = auth;
  }

  ngOnInit(): void {
  }

  ngOnDestroy() {
    console.log(`Spy onDestroy`);
  }

  setUserConnectionInfos(user: User): void {
    this._auth.user$.pipe(map(u => {return new UserConnectionInfos(u.sub, u.email, u.email_verified)}))
      .subscribe({
        next: (u: UserConnectionInfos) => user.connectionInfos = u,
        error: () => {return null;}
        });
  }

  setUserProfile(user: User): void {
    this._userProfileService.getUserProfileById("stub")
    .pipe(
      map(
        u => {return new UserProfile(u.id, u.nickname, u.isCertified, u.country, u.favoriteBeerIds,
          u.connectionInfos)
        }
      )
    )
    .subscribe({
      next : (p: UserProfile) => user.profile = p,
      error : () => {return null;}
    });
  }

  updateUser(user: User): void {
    this.userSource.next(user);
  }

}
/* new UserConnectionInfos(u.connectionInfos.connectionId, u.connectionInfos.email, u.connectionInfos.isEmailVerified) */
