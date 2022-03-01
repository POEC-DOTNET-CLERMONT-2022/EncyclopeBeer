import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Beer } from 'src/models/beer';
import { User } from 'src/models/users/user';
import { SharedBeerService } from 'src/services/shared-beer.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-star',
  templateUrl: './star.component.html',
  styleUrls: ['./star.component.scss']
})
export class StarComponent implements OnInit, OnDestroy{

  private _subscription: Subscription;
  public userService: UserService;
  public sharedBeerService : SharedBeerService;

  public beer: Beer;
  public user: User;

  constructor(userService: UserService, sharedBeerService: SharedBeerService)
  {
    this.userService = userService;
    this.sharedBeerService = sharedBeerService;
  }

  ngOnInit(): void {
    this._subscription = this.userService.user.subscribe((u :User) => {this.user = u; });
    this.beer = this.sharedBeerService.beer;
  }

  ngOnDestroy() {
    this._subscription.unsubscribe();
  }
}


