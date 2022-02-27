import { Component, OnInit, Input } from '@angular/core';
import { Beer } from "../../../models/beer";
import { ActivatedRoute } from "@angular/router";
import { BeerService } from 'src/services/beer.service';
import { User } from 'src/models/users/user';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-beerdetails',
  templateUrl: './beerdetails.component.html',
  styleUrls: ['./beerdetails.component.scss']
})
export class BeerDetailsComponent implements OnInit {

  public beer: Beer;
  public tnull: string = null;

  public userService : UserService;
  public user: User;

  constructor(private activatedRoute: ActivatedRoute, public beerService: BeerService, userService: UserService) {
    this.userService = userService;
   }

  ngOnInit(): void {
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
    this.userService.user.subscribe((u: User) => this.user = u);
    console.log(this.beer);
  }

}
