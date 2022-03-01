export class UserConnectionInfos{

  private _id: string;
  private _email: string;
  private _isVerified: boolean;

  constructor(id: string, email: string, isEmailVerified: boolean) {
    this._id = id;
    this._email = email;
    this._isVerified = isEmailVerified;
  }

  get id(): string {return this._id;}

  get email(): string {return this._email;}

  get isVerified(): boolean {return this._isVerified;}
}
