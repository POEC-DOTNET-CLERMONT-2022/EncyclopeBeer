import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-login-button',
  templateUrl: './login-button.component.html',
  styleUrls: ['./login-button.component.scss'],
})
export class LoginButtonComponent implements OnInit {

  private _authService: AuthService;

  constructor(auth: AuthService)
  {
    this._authService = auth;
  }

  ngOnInit(): void {}

  loginWithRedirect(): void {
    this._authService.loginWithRedirect();
  }
}
