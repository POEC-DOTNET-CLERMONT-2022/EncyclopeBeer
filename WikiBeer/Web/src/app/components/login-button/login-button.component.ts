import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-login-button',
  templateUrl: './login-button.component.html',
  styleUrls: ['./login-button.component.scss'],
})
export class LoginButtonComponent implements OnInit {

  private _authService: AuthService;
  private _userService: UserService;

  constructor(auth: AuthService, userServcice: UserService)
  {
    this._authService = auth;
    this._userService = userServcice;
  }

  ngOnInit(): void {}

  loginWithRedirect(): void {
    console.log("avant connecxion")
    this._authService.loginWithRedirect();
    this._userService.setUserConnectionInfos();
    let user = this._userService.user;
    console.log(user);
    console.log("après connexion")
  }
}
