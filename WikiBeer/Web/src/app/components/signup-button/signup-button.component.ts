import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-signup-button',
  templateUrl: './signup-button.component.html',
  styleUrls: ['./signup-button.component.scss'],
})
export class SignupButtonComponent implements OnInit {

  private _auth: AuthService;

  constructor(auth: AuthService) {
    this._auth = auth;
  }

  ngOnInit(): void {}

  loginWithRedirect(): void {
    this._auth.loginWithRedirect({ screen_hint: 'signup' });
  }
}
