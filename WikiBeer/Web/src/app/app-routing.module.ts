import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListBeersComponent } from './components/listbeers/listbeers.component';
import { BeerDetailsComponent } from './components/beerdetails/beerdetails.component';
import { NavbarComponent } from './components/navbar/navbar.component';

const routes: Routes = [
  {path: NavbarComponent.pathBeerList, component: ListBeersComponent},
  {path: NavbarComponent.pathBeerList+'/:beerId', component: BeerDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
