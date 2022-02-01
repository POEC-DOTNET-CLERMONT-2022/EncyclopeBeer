import { Component, OnInit } from '@angular/core';
import { IBeerList } from 'src/models/i-beer-list';
import { BeerService } from 'src/services/beer.service';
import { Beer } from 'src/models/beer';

@Component({
  selector: 'app-listbeers',
  templateUrl: './listbeers.component.html',
  styleUrls: ['./listbeers.component.scss']
})
export class ListBeersComponent implements OnInit {

  public iBeerList: IBeerList|undefined;
  public beers: Beer[] = [];
  public beerZ: Beer[] = [];
  constructor(public beerService: BeerService) {}

  ngOnInit(): void {
    this.pullBeers(); /* fonctionne */
    this.pullBeerZ(); /* ne fonctionne pas -> Un problème d'async*/
  }

  /* Test GetAll */
  pullBeers()
  {
    this.beerService.getBeers().subscribe((beerList: Beer[]) =>
    {
      this.beers = beerList;
      /* console.log(this.beers); */
      for(let beer of beerList) /* Sa en revanche sa fonctionne */
      {
        this.beerService.getBeerById(beer.id).subscribe((b : Beer) => this.beerZ.push(b));
        console.log(this.beerZ);
        console.log(beer);
      }
    })
  }

  /* Test get par Id */
  pullBeerZ()
  {

    console.log("pull Beerz")
    console.log(this.beers)
    for(let beer of this.beers)
    {
      console.log("for")
      console.log(beer.id)
      this.beerService.getBeerById(beer.id).subscribe((b : Beer) => this.beerZ.push(b));
      console.log(beer);
    }
  }

}
