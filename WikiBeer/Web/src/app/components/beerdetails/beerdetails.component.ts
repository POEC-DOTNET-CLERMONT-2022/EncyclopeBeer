import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Beer } from "../../../models/beer";
import { ActivatedRoute } from "@angular/router";
import { BeerService } from 'src/services/beer.service';
import { User } from 'src/models/users/user';
import { UserService } from 'src/services/user.service';
import { mergeMap, Subscription } from 'rxjs';

@Component({
  selector: 'app-beerdetails',
  templateUrl: './beerdetails.component.html',
  styleUrls: ['./beerdetails.component.scss']
})
export class BeerDetailsComponent implements OnInit, OnDestroy {

  public beer: Beer;
/*   public tnull: string = null; */

  public userService : UserService;
  private _subscription: Subscription;
  public user: User;

  constructor(private activatedRoute: ActivatedRoute, public beerService: BeerService, userService: UserService) {
    this.userService = userService;
   }


  ngOnInit(): void {
    this._subscription = this.userService.user.subscribe((u: User) => this.user = u);
    this.activatedRoute.params.subscribe
    (
      (param) =>
      {
        this.beerService.getBeerById(param['beerId']).subscribe
        (
          (beer: Beer) => {this.beer = beer;}
        );
      }
    )
  }

  ngOnDestroy(): void {
    this._subscription.unsubscribe();
  }

}
