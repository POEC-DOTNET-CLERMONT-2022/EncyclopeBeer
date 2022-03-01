import { HttpErrorResponse } from "@angular/common/http";
import { BehaviorSubject, map, catchError } from "rxjs";
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

  baseUrl: string ='https://localhost:7160/api/';
  userController: string = 'users/';

  private _userProfileService : UserProfileService;
  private _auth: AuthService;

  private userSource = new BehaviorSubject<User>(new User());
  user = this.userSource.asObservable();

  constructor(auth: AuthService, userProfileService : UserProfileService)
  {
    this._userProfileService = userProfileService;
    this._auth = auth;
  }

  ngOnInit(): void {
  }

  ngOnDestroy() {
    console.log(`Spy onDestroy`);
  }

  updateUser(user: User): void {
    this.userSource.next(user);
  }

  setUser(user: User): void {
    this._auth.user$.pipe(map(u => {return new UserConnectionInfos(u.sub, u.email, u.email_verified)}))
      .subscribe({
        next: (u: UserConnectionInfos) => {user.connectionInfos = u;
          this._userProfileService.getUserProfileBySub(user.connectionInfos.id)
          .pipe(
            map(
              u => {
                return new UserProfile(u.connectionInfos, u.id, u.nickname, u.isCertified, u.birthDate ,u.country, u.favoriteBeerIds);
              }
            ),
            catchError(() => {
              let newProfile = new UserProfile(u);
              return this._userProfileService.postUserProfile(newProfile);
            })
          )
          .subscribe({
            next : (p: UserProfile) => {user.profile = p},
            error : (err:HttpErrorResponse) =>
            {
              return null;
            }
          });
        },
        error: (err) => {
              return null;}
        });
  }

  isConnected(user: User): boolean{
    if (user.connectionInfos)
    {
      return true;
    }
    else
    {
      return false;
    }
  }
}
