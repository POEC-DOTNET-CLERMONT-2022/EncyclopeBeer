import { Component, Input, OnInit } from '@angular/core';
import { Beer } from 'src/models/beer';
import { User } from 'src/models/users/user';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-star',
  templateUrl: './star.component.html',
  styleUrls: ['./star.component.scss']
})
export class StarComponent implements OnInit {

  public userService: UserService;

  @Input() beer: Beer;
  public user: User;

  constructor(userService: UserService)
  {
    this.userService = userService;
  }

  ngOnInit(): void {
    this.userService.user.subscribe((u :User) => this.user = u);
    console.log(this.beer);
  }

}
