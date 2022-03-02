import { Component, OnInit, OnDestroy} from '@angular/core';
import { Beer } from "../../../models/beer";
import { ActivatedRoute } from "@angular/router";
import { BeerService } from 'src/services/beer.service';
import { User } from 'src/models/users/user';
import { UserService } from 'src/services/user.service';
import { Subscription } from 'rxjs';
import { SharedBeerService } from 'src/services/shared-beer.service';

@Component({
  selector: 'app-beerdetails',
  templateUrl: './beerdetails.component.html',
  styleUrls: ['./beerdetails.component.scss'],
  providers: [SharedBeerService]
})
export class BeerDetailsComponent implements OnInit, OnDestroy {

  private _subscription: Subscription;

  private _activatedRoute: ActivatedRoute;
  private _beerService: BeerService;
  private _sharedBeerService: SharedBeerService;

  public userService : UserService;

  public user: User;
  public beer: Beer;

  constructor(activatedRoute: ActivatedRoute, beerService: BeerService, sharedBeerService: SharedBeerService, userService: UserService) {
    this._activatedRoute = activatedRoute;
    this._beerService = beerService;
    this._sharedBeerService = sharedBeerService;
    this.userService = userService;
   }

  ngOnInit(): void {
    this._subscription = this.userService.user.subscribe((u: User) => this.user = u);
    this._activatedRoute.params.subscribe
    (
      (param) =>
      {
        this._beerService.getBeerById(param['beerId']).subscribe
        (
          (beer: Beer) => {this.beer = beer; this._sharedBeerService.beer = this.beer;}
        );
      }
    )
  }

  ngOnDestroy(): void {
    this._subscription.unsubscribe();
  }

}
