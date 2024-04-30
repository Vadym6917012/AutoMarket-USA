import { MakeService } from 'src/app/services/make.service';
import { Component, OnInit } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { Car } from 'src/app/models/car/car';
import { CarService } from 'src/app/services/car.service';
import { ModelService } from 'src/app/services/model.service';
import { Model } from 'src/app/models/model/model';
import { Make } from 'src/app/models/make/make';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  customOptions: OwlOptions = {
    loop: true,
    mouseDrag: true,
    touchDrag: true,
    pullDrag: false,
    dots: false,
    margin: 10,
    items: 4,
    autoplay: true,
    navSpeed: 600,
    navText: ['<i class="bi bi-chevron-left" aria-hidden="true"></i>', '<i class="bi bi-chevron-right" aria-hidden="true"></i>'],
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
    nav: true
  }

  homeFormFilter: FormGroup = new FormGroup({});
  cars: Car[] = [];
  makes: Make[] = [];
  models: Model[] = [];

  constructor(private carService: CarService,
    private makeService: MakeService,
    private modelService: ModelService,
    private formBuilder: FormBuilder,
    private router: Router,) { }

  ngOnInit(): void {
    this.initializeForm();

    this.homeFormFilter.get('makeId')?.valueChanges.subscribe((makeId) => {
      this.modelService.getModelsByMake(makeId).subscribe(data => {
        this.models = data;
    })
  });

    this.get5Cars();
    this.getMakes();
    this.getModels();
  }

  initializeForm() {
    this.homeFormFilter = this.formBuilder.group({
      makeId: [''],
      modelId: [''],
      priceFrom: [''],
      priceTo: ['']
    });
  }

    getMakes() {
      this.makeService.getMakes().subscribe((data) => {
        this.makes = data;
      });
    }

    getModels() {
      this.modelService.getModels().subscribe((data) => {
        this.models = data;
      });
    }

    get5Cars() {
      this.carService.getByCount(5).subscribe((data) => {
        this.cars = data;
      });
    }

    filter() {
      const filterValue = this.homeFormFilter.value;
      this.carService.homeFilter(filterValue).subscribe((data) => {
        const cars = data;
        this.router.navigate(['car/car-list'], { state: { cars } });
        console.log('Home component: ')
        console.log(cars);
      });
    }

    filterMake(makeId: number) {
      this.homeFormFilter.get('makeId')?.setValue(makeId);
      this.filter();
    }
  }
