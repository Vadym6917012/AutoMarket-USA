import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Car } from 'src/app/models/car/car';
import { CarService } from 'src/app/services/car.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-car-checkout',
  templateUrl: './car-checkout.component.html',
  styleUrls: ['./car-checkout.component.css']
})
export class CarCheckoutComponent implements OnInit {

  radioGroup: string = 'direct-bank-transfer';
  carId: number;

  car: Car | null = null;
  checkoutForm: FormGroup = new FormGroup({});

  constructor(private carService: CarService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private sharedService: SharedService,) {
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

  updateRadioGroup(value: string) {
    this.radioGroup = value;
  }

  buy() {
    this.sharedService.showNotification(true, "Замовлення оформлено", "Чекайте телефонного дзвінка від адміністратора");
    this.router.navigateByUrl(`/car/car-details/${this.carId}`);
  }

}
