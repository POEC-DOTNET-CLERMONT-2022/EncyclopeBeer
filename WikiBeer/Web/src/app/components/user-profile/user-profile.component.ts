import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  profileJson: string = null;

  private _auth: AuthService;


  constructor(auth: AuthService) {
    this._auth = auth;
  }

  ngOnInit(): void {
    this._auth.user$.subscribe(
      (profile) => (this.profileJson = JSON.stringify(profile, null, 2)),
    );
  }

  get auth(): AuthService {return this._auth;}
}
