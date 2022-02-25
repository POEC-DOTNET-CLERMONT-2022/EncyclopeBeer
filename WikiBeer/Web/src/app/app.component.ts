import { AuthService } from '@auth0/auth0-angular';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  public title = 'WikiBeer';
  private _router: Router;
  private _auth: AuthService;

  constructor(router: Router, auth: AuthService) {
    this._router = router;
    this._auth = auth;
  }

  ngOnInit(): void {
      this._router.navigate([NavbarComponent.pathBeerList]);
  }

  get auth(): AuthService {return this._auth;}

}
