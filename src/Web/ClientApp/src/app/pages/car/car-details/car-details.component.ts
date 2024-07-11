import { ViewportScroller } from '@angular/common';
import { Component, HostListener, Input, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterEvent } from '@angular/router';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { VinCheckResponce } from 'src/app/models/car/VinCheckResponse';
import { Car } from 'src/app/models/car/car';
import { CarService } from 'src/app/services/car.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-car-details',
  templateUrl: './car-details.component.html',
  styleUrls: ['./car-details.component.css']
})
export class CarDetailsComponent implements OnInit {

  carId: number;
  car: Car | null = null;

  vinCheckInfo: VinCheckResponce | null = null;

  relatedCars: Car[] = [];

  customOptions: OwlOptions = {
    loop: false,
    mouseDrag: true,
    touchDrag: true,
    pullDrag: false,
    dots: true,
    items: 1,
    autoplay: true,
    navSpeed: 600,
    responsiveRefreshRate: 200
  }

  customOptionsTopDeal: OwlOptions = {
    loop: false,
    mouseDrag: true,
    touchDrag: true,
    pullDrag: false,
    dots: true,
    items: 4,
    autoplay: true,
    margin: 10,
    navSpeed: 600,
    responsiveRefreshRate: 200,
    responsive: {
      0: {
        items: 1
      },
      400: {
        items: 2
      },
      760: {
        items: 3
      },
      1000: {
        items: 4
      }
    },
  }

  constructor(private carService: CarService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private sharedService: SharedService,
    private viewportScroller: ViewportScroller) {
    this.carId = 0;
    this.activatedRoute.params.subscribe(params => {
      this.carId = +params['id'];
    })
  }

  toTop() {
    this.viewportScroller.scrollToPosition([0, 0]);
  }

  ngOnInit(): void {
    this.getCarById();
    this.fetchRelatedCars();
  }

  getCarById() {
    this.carService.getCar(this.carId).subscribe(data => {
      this.car = data;
    })
  }

  checkVin(vin: string) {
    this.carService.checkVin(vin).subscribe({
      next: (response) => {
        this.vinCheckInfo = response;
        this.sharedService.showVinNotification(response)
      },
      error: (error) => {
        this.vinCheckInfo = error;
        this.sharedService.showVinNotification(error)
      }
    });
  }

  fetchRelatedCars() {
    this.carService.getByCount(5).subscribe(data => {
      this.relatedCars = data;
    })
  }

  navigateToCarDetails(carId: number) {
    this.router.navigate(['/car/car-details', carId]).then(() => {
      location.reload();
    });
  }
}
