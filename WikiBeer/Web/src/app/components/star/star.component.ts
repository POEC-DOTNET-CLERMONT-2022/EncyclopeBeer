import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Beer } from 'src/models/beer';
import { User } from 'src/models/users/user';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-star',
  templateUrl: './star.component.html',
  styleUrls: ['./star.component.scss']
})
export class StarComponent implements OnInit, OnDestroy{

  public userService: UserService;

  @Input() beer: Beer;
  /* @Input() user: User; */
  /* private _beer = new BehaviorSubject<Beer>(null) */

  private _subscription: Subscription;
  public user: User;

  constructor(userService: UserService)
  {
    this.userService = userService;
  }

  ngOnInit(): void {
    /* this._beer.subscribe(() => ); */
    this._subscription = this.userService.user.subscribe((u :User) => this.user = u);
     /* console.log(this.beer); */
  }

  ngOnDestroy() {
    this._subscription.unsubscribe();
  }
  /* get beer(): Beer {return this._beer.getValue()}
  @Input() set beer(value: Beer) {this._beer.next(value);} */


}


/* private _items = new BehaviorSubject<Items[]>([]);

@Input() set items(value: Items[]) {
    this._items.next(value);
}

get items() {
   return this._items.getValue();
}

ngOnInit() {
    this._items.subscribe(x => {
       this.chunk(x);
    })
} */
