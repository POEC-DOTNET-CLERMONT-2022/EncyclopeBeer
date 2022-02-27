import { Country } from "../country";
import { UserConnectionInfos } from "./user-connection-infos";

export class UserProfile{

  private _id : string ='00000000-0000-0000-0000-000000000000';
  private _nickname: string ='';
  private _isCertified: boolean = false;
  private _country: Country = null;
  private _favoriteBeerIds: string[];
 /*  private _connectionString : string = '';
  private _email: string = '';
  private _isEmailVerified: boolean = false; */
  private _connectionInfos : UserConnectionInfos;

  constructor(id: string, nickname: string, isCertified: boolean, country: Country, favoriteBeerIds: string[]
    , connectionInfos : UserConnectionInfos) {
    this._id = id;
    this._nickname = nickname
    this._isCertified = isCertified;
    this._country = country;
    this._favoriteBeerIds = favoriteBeerIds;
    this._connectionInfos = connectionInfos;
  }

  get id(): string {return this._id;}
  set id(value: string) {this._id = value;}

  get nickname(): string {return this._nickname;}
  set nickname(value: string) {this._nickname = value;}

  get isCertified(): boolean {return this._isCertified;}
/*   set id(value: string) {this._id = value;} */

  get country(): Country {return this._country;}
  set country(value: Country) {this._country = value;}

  get favoriteBeerIds(): string[] {return this._favoriteBeerIds;}
  set favoriteBeerIds(value: string[]) {this._favoriteBeerIds = value;}

  get connectionInfos(): UserConnectionInfos {return this._connectionInfos;}

  /* get connectionString(): string {return this._connectionString;} */
  /* set connectionString(value: string) {this._connectionString = value;} */

  /* get email(): string {return this._email;} */
  /* set email(value: string) {this._email = value;} */

  /* get isEmailVerified(): boolean {return this._isEmailVerified;} */

/*   setFromConnectionInfo(userConnectionInfo: UserConnectionInfos): void {
    this._connectionString = userConnectionInfo.connectionString;
    this._email = userConnectionInfo.email;
    this._isEmailVerified = userConnectionInfo.isEmailVerified;
  } */


}

