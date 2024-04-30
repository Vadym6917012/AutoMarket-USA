import { Component, HostListener, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
  vinCheckMessage: string = '';

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
    responsiveRefreshRate : 200
  }

  customOptionsTopDeal: OwlOptions = {
    loop: false,
    mouseDrag: true,
    touchDrag: true,
    pullDrag: false,
    dots: true,
    items: 4,
    autoplay: true,
    navSpeed: 600,
    responsiveRefreshRate : 200
  }

  constructor(private carService: CarService,
     private activatedRoute: ActivatedRoute,
     private router: Router) {
      this.carId = 0;
    this.activatedRoute.params.subscribe(params => {
      this.carId = +params['id'];
    })
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

  checkVin(vin: string){
    this.carService.checkVin(vin).subscribe({
      next: (response) => {
        this.vinCheckMessage = response.message;
      },
      error: (error) => console.error('There was an error!', error)
    });
  }

  fetchRelatedCars(){
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
