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

  private _userService : UserService;
  public user: User;
/*   @Input()
  beer!: Beer;
*/
  constructor(private activatedRoute: ActivatedRoute, public beerService: BeerService, userService: UserService) {
    this._userService = userService;
   }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe
    (
      (param) =>
      {
        this.beerService.getBeerById(param['beerId']).subscribe
        (
          (beer: Beer) => {this.beer = beer; /* console.log(this.beer); console.log(this.beer.name); */}
        );

      }
    )
    this._userService.user.subscribe((u: User) => this.user = u);
    console.log(this.user);
    console.log(this.beer.id)
  }

  isFavoriteBeer() : boolean  {

    console.log(this.user.profile)
    console.log(this.beer)
    if(this.user.profile.favoriteBeerIds.includes(this.beer.id)){
      console.log("est favorite")
      return true;
    }
    else{
      console.log("n'est pas favorite")
      return false;
    }
  }

}
