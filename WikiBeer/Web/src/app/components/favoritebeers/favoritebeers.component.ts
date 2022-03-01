import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Beer } from 'src/models/beer';
import { User } from 'src/models/users/user';
import { UserProfile } from 'src/models/users/user-profile';
import { SharedBeerService } from 'src/services/shared-beer.service';
import { UserProfileService } from 'src/services/user-profile.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-favoritebeers',
  templateUrl: './favoritebeers.component.html',
  styleUrls: ['./favoritebeers.component.scss']
})
export class FavoritebeersComponent implements OnInit {

  private _subscription: Subscription;

  private _activatedRoute: ActivatedRoute;
  private _userService : UserService;

  public userProfileService: UserProfileService;

  public user: User;
  public beers: Beer[] = [];
  public filterTerm!: string;

  constructor(activatedRoute: ActivatedRoute, userService: UserService, userProfileService : UserProfileService) {
    this._activatedRoute = activatedRoute;
    this._userService = userService;
    this.userProfileService = userProfileService;
   }

   ngOnInit(): void {
    this._subscription = this._userService.user.subscribe((u) => this.user = u);
    this.pullFavoriteBeers();
   }

   ngOnDestroy() {
    this._subscription.unsubscribe();
  }

  pullFavoriteBeers() : void
  {
  this.userProfileService.getFavoriteBeers(this.user.profile.id)
  .subscribe((beerList: Beer[]) =>
    {
      this.beers = beerList;
    })
  }

}
