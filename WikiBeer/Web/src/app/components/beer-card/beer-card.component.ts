import { Component, Input, OnInit } from '@angular/core';
import { Beer } from 'src/models/beer';
import { User } from 'src/models/users/user';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-beer-card',
  templateUrl: './beer-card.component.html',
  styleUrls: ['./beer-card.component.scss']
})
export class BeerCardComponent implements OnInit {

  private _userService: UserService;

  @Input() beer: Beer;
  public user: User;

  constructor(userService: UserService)
  {
    this._userService = userService
  }

  ngOnInit(): void {
    this._userService.user.subscribe((u :User) => this.user = u);
  }


  isFavoriteBeer() : boolean  {

    console.log(this.user.profile)
    console.log(this.beer)
    if(this.user.profile.favoriteBeerIds.includes(this.beer.id)){
      return true;
    }
    else{
      return false;
    }
  }

}
