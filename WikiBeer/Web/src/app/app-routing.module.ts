import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListBeersComponent } from './listbeers/listbeers.component';

const routes: Routes = [
  {path: 'beers', component: ListBeersComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
