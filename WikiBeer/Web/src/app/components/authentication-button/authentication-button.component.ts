import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-authentication-button',
  templateUrl: './authentication-button.component.html',
  styleUrls: ['./authentication-button.component.scss'],
})

export class AuthenticationButtonComponent implements OnInit {
  private _auth: AuthService
  constructor(auth: AuthService) {
    this._auth = auth;
  }

  ngOnInit(): void {}

  get auth(): AuthService {return this._auth;}
/*   set auth(value: AuthService){this._auth = value;} */
}
