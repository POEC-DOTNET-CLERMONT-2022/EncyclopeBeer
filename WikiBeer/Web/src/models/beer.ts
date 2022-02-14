import { Brewery } from "./brewery";
import { Color } from "./color";
import { Ingredient } from "./ingredients/ingredient";
import { Style } from "./style";

export class Beer
{
  private _id : string = '';
  private _name: string ='';
  private _description: string ='';
  private _ibu: number = 0;
  private _degree: number = 0;
  private _brewery: Brewery;
  private _style: Style;
  private _color: Color;
  private _ingredients: Ingredient[] = []

  constructor(id: string, name: string, description: string, ibu: number, degree: number, brewery: Brewery,
    style: Style, color: Color, ingredient: Ingredient) {
    this._id = id;
    this._name = name;
    this._description = description;
    this._ibu = ibu;
    this._degree = degree;
    this._brewery = brewery;
    this._style = style;
    this._color = color;
    this._ingredients;
  }

  get id(): string {return this._id;}
  set id(value: string) {this._id = value;}

  get name(): string {return this._name;}
  set name(value: string) {this._name = value;}

  get description(): string {return this._description;}
  set description(value: string) {this._description = value;}

  get ibu(): number {return this._ibu;}
  set ibu(value: number) {this._ibu = value;}

  get degree(): number {return this._degree;}
  set degree(value: number) {this._degree = value;}

  get brewery(): Brewery {return this._brewery;}
  set brewery(value: Brewery) {this._brewery = value;}

  get style(): Style {return this._style;}
  set style(value: Style) {this._style = value;}

  get color(): Color {return this._color;}
  set color(value: Color) {this._color = value;}

  get ingredients(): Ingredient[] {return this._ingredients;}
  set ingredients(value: Ingredient[]) {this._ingredients = value;}

}
