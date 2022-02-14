export abstract class Ingredient{

  protected _id: string = '';
  protected _name: string = '';
  protected _description: string ='';

  protected constructor(id: string, name: string, description: string)
  {
    this._id = id;
    this._name = name;
    this._description = description;
  }

  get id(): string {return this._id;}
  set id(value: string) {this._id = value;}

  get name(): string {return this._name;}
  set name(value: string) {this._name = value;}

  get description(): string {return this._description;}
  set description(value: string) {this._description = value;}

}
