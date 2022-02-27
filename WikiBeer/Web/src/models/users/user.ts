import { UserConnectionInfos } from "./user-connection-infos";
import { UserProfile } from "./user-profile";

export class User{

  private _connectionInfos: UserConnectionInfos;
  private _profile: UserProfile;

  constructor(userConnectionInfos: UserConnectionInfos = null, userProfile : UserProfile = null) {
    this._connectionInfos = userConnectionInfos;
    this._profile = userProfile;
  }

  get connectionInfos (): UserConnectionInfos {return this._connectionInfos;}
  set connectionInfos (value: UserConnectionInfos) {this._connectionInfos = value;}

  get profile () : UserProfile {return this._profile;}
  set profile (value: UserProfile) {this._profile = value;}
}
