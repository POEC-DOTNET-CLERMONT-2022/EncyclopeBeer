import { Country } from "../country";
import { IUserConnectionInfos } from "./i-user-connection-infos";

export class UserProfile{

  private _id : string ='';
  private _nickname: string ='';
  private _isCertified: boolean = false;
  private _country: Country;
  private _userBeers: string[];
  private _sub : string = '';
  private _email: string = '';
  private _isEmailVerified: boolean = false;

  constructor(id: string, nickname: string, isCertified: boolean, country: Country, userBeers: string[]
    , userConnectionInfo : IUserConnectionInfos) {
    this._id = id;
    this._nickname = nickname
    this._isCertified = isCertified;
    this._country = country;
    this._userBeers = userBeers;
    this.setFromConnectionInfo(userConnectionInfo);
  }

  get id(): string {return this._id;}
  set id(value: string) {this._id = value;}

  get nickname(): string {return this._nickname;}
  set nickname(value: string) {this._nickname = value;}

  get isCertified(): boolean {return this._isCertified;}
/*   set id(value: string) {this._id = value;} */

  get country(): Country {return this._country;}
  set country(value: Country) {this._country = value;}

  get userBeers(): string[] {return this._userBeers;}
  set userBeers(value: string[]) {this._userBeers = value;}

  get sub(): string {return this._sub;}
  /* set sub(value: string) {this._sub = value;} */

  get email(): string {return this._email;}
  /* set email(value: string) {this._email = value;} */

  get isEmailVerified(): boolean {return this._isEmailVerified;}

  setFromConnectionInfo(userConnectionInfo: IUserConnectionInfos): void {
    this._sub = userConnectionInfo.sub;
    this._email = userConnectionInfo.email;
    this._isEmailVerified = userConnectionInfo.email_verified;
  }


}

