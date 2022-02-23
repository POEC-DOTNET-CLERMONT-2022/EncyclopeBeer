import { AuthService } from '@auth0/auth0-angular';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {

  private _router: Router;
  private _auth: AuthService;

  static pathBeerList : string = 'beers';
  urlBeerList : string = '/' + NavbarComponent.pathBeerList;

  static pathUserProfile : string = 'account';
  urlUserProfile : string = '/' + NavbarComponent.pathUserProfile;


  constructor(router: Router, auth: AuthService)
  {
    this._router = router;
    this._auth = auth;
  }

  get auth(): AuthService {return this._auth;}

}
