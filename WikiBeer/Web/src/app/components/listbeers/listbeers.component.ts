import { Component, OnInit } from '@angular/core';
import { BeerService } from 'src/services/beer.service';
import { Beer } from 'src/models/beer';

@Component({
  selector: 'app-listbeers',
  templateUrl: './listbeers.component.html',
  styleUrls: ['./listbeers.component.scss']
})
export class ListBeersComponent implements OnInit {

  beers: Beer[] = [];
  filterTerm!: string;

  constructor(public beerService: BeerService) {}

  ngOnInit(): void {
    this.pullBeers(); /* fonctionne */
  }

  /* Test GetAll */
  pullBeers()
  {
    this.beerService.getBeers().subscribe((beerList: Beer[]) =>
    {
      this.beers = beerList;
    })
  }
}
