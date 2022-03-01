import { FormControl, Validators, FormGroup } from '@angular/forms';
import { UserService } from 'src/services/user.service';
import { CountryService } from 'src/services/country.service';
import { User } from 'src/models/users/user';
import { Country } from 'src/models/country';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';


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
  public countries: Country[] = [];

  userForm! :FormGroup;

  private _userService : UserService;
  private _countryService : CountryService;

  constructor(userService : UserService, countryService : CountryService)
  {
    this._userService = userService;
    this._countryService = countryService;
    this.userService = userService;
  }

  ngOnInit(): void {
    this.pullCountries();
    this._userService.user.subscribe((u) => this.user = u)
    this._userService.trySetUserProfile(this.user);
    this._userService.updateUser(this.user);
    console.log(this.user)
    this.userForm = new FormGroup(
      {
          nickname: new FormControl(),
          country: new FormControl()
      }
    )

  }

  pullCountries()
  {
    this._countryService.getCountries().subscribe((countryList: Country[]) =>
    {
      this.countries = countryList;
    })
  }

  onSubmit() {
    console.log(this.userForm.value)
    this._subscription = this.userService.user.subscribe((u : User) => this.user = u);
    console.log(this.user);
  }

  ngOnDestroy(): void {
    this._subscription.unsubscribe();
  }
}
