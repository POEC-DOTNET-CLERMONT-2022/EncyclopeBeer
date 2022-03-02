import { AuthService } from '@auth0/auth0-angular';
import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar.component';
import { UserService } from 'src/services/user.service';
import { User } from 'src/models/users/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  public title = 'WikiBeer';
  private _router: Router;
  private userService: UserService;
  public user: User;

  constructor(router: Router, userService: UserService) {
    this._router = router;
    this.userService = userService;
  }

  ngOnInit(): void {

      this.userService.user.subscribe((u) => this.user = u);
      this.performInitialNavigation(this.user);
    }

    performInitialNavigation(user: User): void
    {
      if (this.userService.isConnected(this.user)){
        this._router.navigate([NavbarComponent.pathUserProfile]);
      }
      else{
        this._router.navigate([NavbarComponent.pathBeerList]);
      }
    }

}
