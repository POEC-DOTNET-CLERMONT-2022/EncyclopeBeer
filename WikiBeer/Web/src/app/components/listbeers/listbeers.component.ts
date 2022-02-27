import { Component, OnInit } from '@angular/core';
import { BeerService } from 'src/services/beer.service';
import { Beer } from 'src/models/beer';
import { AuthService } from '@auth0/auth0-angular';

import { UserService } from 'src/services/user.service';
import { User } from 'src/models/users/user';

@Component({
  selector: 'app-listbeers',
  templateUrl: './listbeers.component.html',
  styleUrls: ['./listbeers.component.scss']
})
export class ListBeersComponent implements OnInit {

  public beers: Beer[] = [];
  filterTerm!: string;
  private _beerService : BeerService;
  private _userService : UserService;
  private _auth : AuthService;
  public user : User;

  constructor(beerService: BeerService, userService: UserService) {
    this._beerService = beerService;
    this._userService = userService;
  }

  ngOnInit(): void {
    this.pullBeers(); /* fonctionne */
    this._userService.user.subscribe((u) => this.user = u);
    this._userService.setUserConnectionInfos(this.user);
    this._userService.trySetUserProfile(this.user);
    this._userService.updateUser(this.user);
  }

  /* Test GetAll */
  pullBeers()
  {
    this._beerService.getBeers().subscribe((beerList: Beer[]) =>
    {
      this.beers = beerList;
    })
  }
}
