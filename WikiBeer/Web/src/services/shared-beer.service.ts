import { Injectable } from '@angular/core';
import { Beer } from 'src/models/beer';

@Injectable({
  providedIn: 'root'
})
export class SharedBeerService {

  _beer: Beer;
  constructor() { }

  get beer(): Beer {return this._beer;}
  set beer(value: Beer) {this._beer = value;}
}
