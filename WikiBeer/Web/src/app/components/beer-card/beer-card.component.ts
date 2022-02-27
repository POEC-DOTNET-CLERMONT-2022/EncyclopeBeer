import { Component, Input, OnInit } from '@angular/core';
import { Beer } from 'src/models/beer';

@Component({
  selector: 'app-beer-card',
  templateUrl: './beer-card.component.html',
  styleUrls: ['./beer-card.component.scss']
})
export class BeerCardComponent implements OnInit {

  @Input() beer: Beer;

  constructor()
  {
  }

  ngOnInit(): void {
  }

}
