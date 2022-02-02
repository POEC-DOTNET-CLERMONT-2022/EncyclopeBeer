import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Beer } from "src/models/beer";

import { Injectable } from '@angular/core';
import { IBeerList } from "src/models/i-beer-list";

@Injectable({
  providedIn: 'root'
})
export class BeerService {
  /* ici la connection string de l'api*/
  baseUrl: string ='https://localhost:7160/api/';
  beerController: string = 'beers/';
  /* hhtpClient par injection de dépendance */
  constructor(private httpClient: HttpClient) { }

  getBeers(): Observable<Beer[]>
  {
      return this.httpClient.get<Beer[]>(this.baseUrl + this.beerController);
  }



/*   getBeers(): Observable<IBeerList>
  {
      return this.httpClient.get<IBeerList>(this.baseUrl + this.beerController);
  } */

  getBeerById(beerId : string): Observable<Beer>
  {
      return this.httpClient.get<Beer>(this.baseUrl+this.beerController+beerId);
  }
}
