import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {

  private _router: Router;

  static pathBeerList : string = 'beers';
  urlBeerList : string = '/' + NavbarComponent.pathBeerList;



  constructor(router: Router)
  {
    this._router = router;
  }


}
