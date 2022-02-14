import { Ingredient } from "./ingredient";

export class Cereal extends Ingredient{

  private _ebc: number = 0;

  constructor(id: string, name: string, description: string, ebc: number)
  {
    super(id, name, description);
    this._ebc = ebc;
  }

  get ebc(): number {return this._ebc;}
  set ebc(value: number) {this._ebc = value;}
}
