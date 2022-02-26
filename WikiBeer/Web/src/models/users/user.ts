import { IUserConnectionInfos } from "./i-user-connection-infos";
import { UserProfile } from "./user-profile";

export class User{

  private _userConnectionInfos: IUserConnectionInfos;
  private _userProfile: UserProfile;

  constructor(userConnectionInfos: IUserConnectionInfos = null, userProfile : UserProfile = null) {
    this._userConnectionInfos = userConnectionInfos;
    this._userProfile = userProfile;
  }

  setConnectionInfo(userConnectionInfos: IUserConnectionInfos){
    this._userConnectionInfos = userConnectionInfos;
  }

}
