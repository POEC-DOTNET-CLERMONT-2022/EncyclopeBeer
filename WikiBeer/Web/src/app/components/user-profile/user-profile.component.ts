import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { UserService } from 'src/services/user.service';
import { CountryService } from 'src/services/country.service';
import { User } from 'src/models/users/user';
import { Country } from 'src/models/country';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  public user : User;
  public countries: Country[] = [];

  userForm! :FormGroup;

  private _userService : UserService;
  private _countryService : CountryService;

  constructor(userService : UserService, countryService : CountryService)
  {
    this._userService = userService;
    this._countryService = countryService;
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
  }

}
