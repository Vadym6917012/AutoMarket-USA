import { FormsModule } from '@angular/forms';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarListComponent } from './car-list/car-list.component';
import { CarRoutingModule } from './car-routing.module';
import { CarAddComponent } from './car-add/car-add.component';
import { CarDetailsComponent } from './car-details/car-details.component';
import { CarUpdateComponent } from './car-update/car-update.component';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { PaginationComponent } from './pagination/pagination.component';
import { CarCheckoutComponent } from './car-checkout/car-checkout.component';

@NgModule({
  declarations: [
    CarListComponent,
    CarAddComponent,
    CarDetailsComponent,
    CarUpdateComponent,
    PaginationComponent,
    CarCheckoutComponent,
  ],
  imports: [
    CommonModule,
    CarRoutingModule,
    SharedModule,
    FormsModule,
    CarouselModule,
  ]
})
export class CarModule { }