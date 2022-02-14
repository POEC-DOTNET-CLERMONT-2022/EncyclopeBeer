import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListBeersComponent } from './listbeers/listbeers.component';
import { BeerDetailsComponent } from './beerdetails/beerdetails.component';

const routes: Routes = [
  {path: 'beers', component: ListBeersComponent},
  {path: 'beers'+'/:beerId', component: BeerDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
