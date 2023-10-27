import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarListComponent } from './car-list/car-list.component';
import { CarRoutingModule } from './car-routing.module';

@NgModule({
  declarations: [
    CarListComponent
  ],
  imports: [
    CommonModule,
    CarRoutingModule,
    SharedModule
  ]
})
export class CarModule { }