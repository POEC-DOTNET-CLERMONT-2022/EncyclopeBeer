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
export class AppComponent implements OnInit, OnChanges{

  public title = 'WikiBeer';
  private _router: Router;
  private _userService: UserService;
  private _user: User;

  constructor(router: Router, userService: UserService) {
    this._router = router;
    this._userService = userService;
  }

  ngOnChanges(changes: SimpleChanges): void {
    this._router.navigate([NavbarComponent.pathBeerList]);
/*     this._userService.user.subscribe((u) => this._user = u);
    this._userService.setUserConnectionInfos(this._user);
    this._userService.setUserProfile(this._user);
    this._userService.updateUser(this._user); */
  }


  ngOnInit(): void {
      let tt = this._router.navigate([NavbarComponent.pathBeerList]);
      console.log(tt);
      this._userService.user.subscribe((u) => this._user = u);
      this._userService.setUserConnectionInfos(this._user);
      this._userService.setUserProfile(this._user);
      this._userService.updateUser(this._user);
    }


}
