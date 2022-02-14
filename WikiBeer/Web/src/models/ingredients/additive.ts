import { Ingredient } from "./ingredient";

export class Additive extends Ingredient{

  private _use: string = '';

  constructor(id: string, name: string, description: string, use: string)
  {
    super(id, name, description);
    this._use = use;
  }

  get use(): string {return this._use;}
  set use(value: string) {this._use = value;}
}
