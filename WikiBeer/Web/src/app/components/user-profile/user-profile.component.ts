import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/services/user.service';
import { User } from 'src/models/users/user';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  public user : User;

  private _userService : UserService;

  constructor(userService : UserService)
  {
    this._userService = userService;
  }

  ngOnInit(): void {
    this._userService.user.subscribe((u) => this.user = u);
    this._userService.setUserConnectionInfos;
    this._userService.trySetUserProfile;
    console.log(this.user)
  }
}
