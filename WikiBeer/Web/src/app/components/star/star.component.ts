import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { Subscription } from 'rxjs';
import { Beer } from 'src/models/beer';
import { User } from 'src/models/users/user';
import { SharedBeerService } from 'src/services/shared-beer.service';
import { UserProfileService } from 'src/services/user-profile.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-star',
  templateUrl: './star.component.html',
  styleUrls: ['./star.component.scss']
})
export class StarComponent implements OnInit, OnChanges, OnDestroy{

  private _subscription: Subscription;
  public userService: UserService;
  public _sharedBeerService : SharedBeerService;
  private _userProfileService: UserProfileService;

  public beer: Beer;
  public user: User;

  constructor(userService: UserService, sharedBeerService: SharedBeerService, userProfileService : UserProfileService)
  {
    this.userService = userService;
    this._sharedBeerService = sharedBeerService;
    this._userProfileService = userProfileService;
  }

  ngOnChanges(changes: SimpleChanges): void {
  }

  ngOnInit(): void {
    this._subscription = this.userService.user.subscribe((u :User) => {this.user = u; });
    this.beer = this._sharedBeerService.beer;
  }

  ngOnDestroy() {
    this._subscription.unsubscribe();
  }

  switchFavoriteState() {
    if (this.isFavorite())
    {
      this.removeBeerFromFavorites();
    }
    else
    {
      this.addBeerToFavorites();
    }
    this._userProfileService.putUserProfile(this.user.profile).subscribe();
    this.userService.updateUser(this.user);
    console.log(this.user)
  }

  addBeerToFavorites(){
    this.user.profile.favoriteBeerIds.push(this.beer.id);
  }

  removeBeerFromFavorites() {
    const index: number = this.user.profile.favoriteBeerIds.indexOf(this.beer.id);
    this.user.profile.favoriteBeerIds.splice(index, 1);
  }

  isFavorite(): boolean {
    return this.user.profile.isIdInFavorites(this.beer.id);
  }

}


