import { Component, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { BeerService } from 'src/services/beer.service';
import { Beer } from 'src/models/beer';
import { AuthService } from '@auth0/auth0-angular';

import { UserService } from 'src/services/user.service';
import { User } from 'src/models/users/user';
import { map, mergeMap, Subscription } from 'rxjs';

@Component({
  selector: 'app-listbeers',
  templateUrl: './listbeers.component.html',
  styleUrls: ['./listbeers.component.scss']
})
export class ListBeersComponent implements OnInit, OnDestroy{


  private _beerService : BeerService;
  private _userService : UserService;
  private _subscription: Subscription;

  public user : User;
  public beers: Beer[] = [];
  public filterTerm!: string;

  constructor(beerService: BeerService, userService: UserService) {
    this._beerService = beerService;
    this._userService = userService;
  }

  ngOnInit(): void {
    this.pullBeers();
    this._subscription = this._userService.user.subscribe((u) => this.user = u);
    this._userService.setUser(this.user);
    this._userService.updateUser(this.user);
  }

  ngOnDestroy() {
    this._subscription.unsubscribe();
  }

  pullBeers() : void
  {
  this._beerService.getBeers()
  .subscribe((beerList: Beer[]) =>
    {
      this.beers = beerList;
    })
  }

}
