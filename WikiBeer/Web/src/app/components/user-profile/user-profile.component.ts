import { Component, OnInit } from '@angular/core';
import { AuthService, User } from '@auth0/auth0-angular';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  profileJson: string = null;

  private _auth: AuthService;
  public user : User;

  constructor(auth: AuthService) {
    this._auth = auth;
  }

  ngOnInit(): void {
    this._auth.user$.subscribe(
      (profile) => (this.profileJson = JSON.stringify(profile, null, 2)),
      );
    this._auth.user$.subscribe((u) => this.user = u);
    this._auth.user$.subscribe((u) => console.log(u));
  }

  get auth(): AuthService {return this._auth;}
}
