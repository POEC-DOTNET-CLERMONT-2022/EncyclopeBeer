import { Country } from "./country";

export class Brewery{

  private _id: string = '';
  private _name: string = '';
  private _description: string ='';
  private _country: Country|undefined;

  constructor(id: string, name: string, description: string, country: Country) {
    this._id = id;
    this._name = name;
    this._description = description;
    this._country = country;
  }

  get id(): string {return this._id;}
  set id(value: string) {this._id = value;}

  get name(): string {return this._name;}
  set name(value: string) {this._name = value;}

  get description(): string {return this._description;}
  set description(value: string) {this._description = value;}

  get country(): Country|undefined {return this._country;}
  set country(value: Country|undefined) {this._country = value;}
}
