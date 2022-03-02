import { HttpClient } from "@angular/common/http";
import { map, Observable } from "rxjs";
import { Beer } from "src/models/beer";

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BeerService {

  baseUrl: string ='https://localhost:7160/api/';
  beerController: string = 'beers/';

  constructor(private httpClient: HttpClient) { }

  getBeers(): Observable<Beer[]>
  {
      return this.httpClient.get<Beer[]>(this.baseUrl + this.beerController);
  }

  getBeerById(beerId : string): Observable<Beer>
  {
      return this.httpClient.get<Beer>(this.baseUrl+this.beerController+beerId).pipe(map((b: Beer) => { return b;}));
  }
}
