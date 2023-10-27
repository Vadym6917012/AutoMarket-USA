import { NgModule } from '@angular/core';
import { CarListComponent } from './car-list/car-list.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: 'car-list', component: CarListComponent},

]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ], 
  exports: [
    RouterModule
  ]
})
export class CarRoutingModule { }