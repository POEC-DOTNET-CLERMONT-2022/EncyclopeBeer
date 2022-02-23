export class User{

  private _id : string = '';
  private _nickname: string ='';
  private _email: string = '';


  constructor(id: string, nickname: string, email: string) {
    this._id = id;
    this._nickname = nickname
    this._email = email;
  }

  get id(): string {return this._id;}
  set id(value: string) {this._id = value;}

  get nickname(): string {return this._nickname;}
  set nickname(value: string) {this._nickname = value;}

  get email(): string {return this._email;}
  set email(value: string) {this._email = value;}

}
