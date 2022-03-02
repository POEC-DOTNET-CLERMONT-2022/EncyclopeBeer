import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Country } from "src/models/country";

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  baseUrl: string = 'https://localhost:7160/api/';
  countryController: string = 'countries/'

  constructor(private httpClient: HttpClient) { }

  getCountries(): Observable<Country[]>
  {
    return this.httpClient.get<Country[]>(this.baseUrl + this.countryController);
  }
}
