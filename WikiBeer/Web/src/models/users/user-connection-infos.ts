export class UserConnectionInfos{

  private _connectionId: string;
  private _email: string;
  private _isVerified: boolean;

  constructor(connectionId: string, email: string, isEmailVerified: boolean) {
    this._connectionId = connectionId;
    this._email = email;
    this._isVerified = isEmailVerified;
  }

  get connectionId(): string {return this._connectionId;}

  get email(): string {return this._email;}

  get isEmailVerified(): boolean {return this._isVerified;}
}
