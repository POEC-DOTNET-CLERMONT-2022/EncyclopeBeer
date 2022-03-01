import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { Subscription } from 'rxjs';
import { Beer } from 'src/models/beer';
import { User } from 'src/models/users/user';
import { SharedBeerService } from 'src/services/shared-beer.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-beer-card',
  templateUrl: './beer-card.component.html',
  styleUrls: ['./beer-card.component.scss'],
  providers: [SharedBeerService]
})
export class BeerCardComponent implements OnInit, OnChanges, OnDestroy {

  public userService: UserService;
  public sharedBeerService: SharedBeerService;

  private _subscription: Subscription;

  @Input() beer: Beer;
  public user: User;

  constructor(userService: UserService, sharedBeerService: SharedBeerService)
  {
    this.userService = userService;
    this.sharedBeerService = sharedBeerService;
  }

  ngOnInit(): void {
    this._subscription = this.userService.user.subscribe((u :User) => {this.user = u;});
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.sharedBeerService.beer = this.beer;;
  }

  ngOnDestroy() {
    this._subscription.unsubscribe();
  }

}
