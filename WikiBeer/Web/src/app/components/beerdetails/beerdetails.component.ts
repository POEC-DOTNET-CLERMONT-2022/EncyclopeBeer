import { Component, OnInit, Input } from '@angular/core';
import { Beer } from "../../../models/beer";
import { ActivatedRoute } from "@angular/router";
import { BeerService } from 'src/services/beer.service';

@Component({
  selector: 'app-beerdetails',
  templateUrl: './beerdetails.component.html',
  styleUrls: ['./beerdetails.component.scss']
})
export class BeerDetailsComponent implements OnInit {

  public beer: Beer;
  public tnull: string = null;
/*   @Input()
  beer!: Beer;
*/
  constructor(private activatedRoute: ActivatedRoute, public beerService: BeerService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe
    (
      (param) =>
      {
        this.beerService.getBeerById(param['beerId']).subscribe
        (
          (beer: Beer) => {this.beer = beer; /* console.log(this.beer); console.log(this.beer.name); */}
        );

      }
    )
  }

}
