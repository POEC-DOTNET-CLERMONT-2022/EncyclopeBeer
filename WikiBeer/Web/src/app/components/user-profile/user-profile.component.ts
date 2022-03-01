import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { User } from 'src/models/users/user';
import { UserService } from 'src/services/user.service';


@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit, OnDestroy {

  profileJson: string = null;

  private _subscription: Subscription;
  public userService: UserService;
  public user : User;

  constructor(userService: UserService) {
    this.userService = userService;
  }

  ngOnInit(): void {
    this._subscription = this.userService.user.subscribe((u : User) => this.user = u);
    console.log(this.user);
  }

  ngOnDestroy(): void {
    this._subscription.unsubscribe();
  }


}
