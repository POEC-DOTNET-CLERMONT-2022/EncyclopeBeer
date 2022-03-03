import { FormControl, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { UserService } from 'src/services/user.service';
import { CountryService } from 'src/services/country.service';
import { User } from 'src/models/users/user';
import { Country } from 'src/models/country';
import { Component, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import {UserProfileService} from 'src/services/user-profile.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit, OnChanges, OnDestroy {

  profileJson: string = null;

  private _subscription: Subscription;
  public userService: UserService;
  public user : User;
  public countries: Country[] = [];

  userForm! :FormGroup;

  private _userService : UserService;
  private _countryService : CountryService;
  private _profileService : UserProfileService

  constructor(userService : UserService, countryService : CountryService, profileService : UserProfileService)
  {
    this._userService = userService;
    this._countryService = countryService;
    this.userService = userService;
    this._profileService = profileService;
  }

  ngOnChanges(): void {
    this._subscription = this._userService.user.subscribe((u) => this.user = u)
  }

  ngOnInit(): void {
    this.pullCountries();
    this._subscription = this._userService.user.subscribe((u) => this.user = u)
    /* setTimeout(() => {  console.log("World!"); }, 20000); */
    console.log(this.user)
    this.userForm = new FormGroup(
      {
          _nickname: new FormControl(
            this.user.profile.nickname
          ),
          _country: new FormControl(
            this.user.profile.country.name
          ),
          _birthdate: new FormControl(
            new Date(this.user.profile.birthDate)
          )
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
    this.user.profile.nickname = this.userForm.value._nickname;
    let country = this.countries.find(c => c.name === this.userForm.value.country)
    this.user.profile.country = country;
    this.user.profile.birthDate = this.userForm.value._birhDate;
    console.log(this.user);
    this._userService.updateUser(this.user);
    this._profileService.putUserProfile(this.user.profile);

  }

  ngOnDestroy(): void {
    this._subscription.unsubscribe();
  }

  get nickname(): AbstractControl{
    return this.getAbstractControlByFieldName('_nickname')
  }

  get country(): AbstractControl{
    return this.getAbstractControlByFieldName('_country')
  }

  get birthdate(): AbstractControl{
    return this.getAbstractControlByFieldName('_birthdate')
  }

  getAbstractControlByFieldName(field: string): AbstractControl {
    return <AbstractControl>this.userForm.get(field);
  }
}
