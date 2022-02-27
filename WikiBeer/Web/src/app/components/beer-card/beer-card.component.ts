import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Beer } from 'src/models/beer';
import { User } from 'src/models/users/user';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-beer-card',
  templateUrl: './beer-card.component.html',
  styleUrls: ['./beer-card.component.scss']
})
export class BeerCardComponent implements OnInit, OnDestroy {

  public userService: UserService;
  public _subscription: Subscription;

  @Input() beer: Beer;
  public user: User;

  constructor(userService: UserService)
  {
    this.userService = userService;
  }

  ngOnInit(): void {
    console.log("dans init")
    this._subscription = this.userService.user.subscribe((u :User) => this.user = u);
  }

  ngOnDestroy() {
    this._subscription.unsubscribe();
  }

}
