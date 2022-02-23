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

    /**
     *
     */
    constructor(router: Router) {
      this._router = router;
    }

    ngOnInit(): void {
        this._router.navigate([NavbarComponent.pathBeerList]);
    }
}
