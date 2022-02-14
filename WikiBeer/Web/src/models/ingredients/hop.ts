import { Ingredient } from "./ingredient";

export class Hop extends Ingredient{

  private _alphaAcid: number = 0;

  constructor(id: string, name: string, description: string, alphaAcid: number)
  {
    super(id, name, description);
    this._alphaAcid = alphaAcid;
  }

  get alphaAcid(): number {return this._alphaAcid;}
  set alphaAcid(value: number) {this._alphaAcid = value;}
}
