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

  public userService: UserService;

  @Input() beer: Beer;
  public user: User;

  constructor(userService: UserService)
  {
    this.userService = userService;
  }

  ngOnInit(): void {
    this.userService.user.subscribe((u :User) => this.user = u);
  }

/*   isFavoriteBeer() : boolean  {
    if(this.user.profile.favoriteBeerIds.includes(this.beer.id)){
      return true;
    }
    else{
      return false;
    }
  }
 */
}
