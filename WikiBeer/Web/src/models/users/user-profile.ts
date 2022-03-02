import { Country } from "../country";
import { UserConnectionInfos } from "./user-connection-infos";

export class UserProfile{

  private _id : string;
  private _nickname: string;
  private _isCertified: boolean;
  private _birthDate: Date;
  private _country: Country;
  private _favoriteBeerIds: string[];
  private _connectionInfos : UserConnectionInfos;

  constructor(connectionInfos : UserConnectionInfos,
    id: string = '00000000-0000-0000-0000-000000000000',
    nickname: string = '',
    isCertified: boolean = false,
    birthDate: Date = new Date(2000,1,1),
    country: Country = null,
    favoriteBeerIds: string[] = []) {
    this._id = id;
    this._nickname = nickname
    this._isCertified = isCertified;
    this._birthDate = birthDate;
    this._country = country;
    this._favoriteBeerIds = favoriteBeerIds;
    this._connectionInfos = connectionInfos;
  }

  get id(): string {return this._id;}
  set id(value: string) {this._id = value;}

  get nickname(): string {return this._nickname;}
  set nickname(value: string) {this._nickname = value;}

  get isCertified(): boolean {return this._isCertified;}

  get birthDate(): Date {return this._birthDate;}
  set birthDate(value : Date) {this._birthDate = value;}

  get country(): Country {return this._country;}
  set country(value: Country) {this._country = value;}

  get favoriteBeerIds(): string[] {return this._favoriteBeerIds;}
  set favoriteBeerIds(value: string[]) {this._favoriteBeerIds = value;}

  get connectionInfos(): UserConnectionInfos {return this._connectionInfos;}

  isIdInFavorites(id: string): boolean
  {
    return this.favoriteBeerIds.includes(id);
  }

}

