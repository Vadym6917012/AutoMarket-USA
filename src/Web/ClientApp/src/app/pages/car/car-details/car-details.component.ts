import { Component, HostListener, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { Car } from 'src/app/models/car/car';
import { CarService } from 'src/app/services/car.service';

@Component({
  selector: 'app-car-details',
  templateUrl: './car-details.component.html',
  styleUrls: ['./car-details.component.css']
})
export class CarDetailsComponent implements OnInit {

  carId: number;
  car: Car | null = null;

  customOptions: OwlOptions = {
    loop: false,
    mouseDrag: true,
    touchDrag: true,
    pullDrag: false,
    dots: true,
    items: 1,
    autoplay: true,
    navSpeed: 600,
    responsiveRefreshRate : 200
  }

  constructor(private carService: CarService,
     private activatedRoute: ActivatedRoute) {
      this.carId = 0;
    this.activatedRoute.params.subscribe(params => {
      this.carId = +params['id'];
    })
  }

  ngOnInit(): void {
    this.getCarById();
  }

  getCarById() {
    this.carService.getCar(this.carId).subscribe(data => {
      this.car = data;
    })
  }
}
