import { Component, OnInit, SimpleChanges } from '@angular/core';
import { Car } from 'src/app/models/car/car';
import { Image } from 'src/app/models/photos/image';
import { CarService } from 'src/app/services/car.service';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css']
})
export class CarListComponent implements OnInit {
  carInfo: any | null = null;
  carImages: { [carId: number]: Image[] } = {};
  isListView = true;
  selectedView = 'list';
  selectedSortOption = '';

  constructor(private carService: CarService,
    private imageService: ImageService) {
    const navigation = window.history.state;

    if (navigation && navigation.cars) {
      this.carInfo = navigation.cars;
    } else {
      this.showCars();
    }
  }

  sortCars() {
    if (this.selectedSortOption === 'default') {
      this.showCars();
    } else if (this.selectedSortOption === 'asc') {
      this.carInfo.sort((a: { price: number }, b: { price: number }) => a.price - b.price);
    } else if (this.selectedSortOption === 'desc') {
      this.carInfo.sort((a: { price: number }, b: { price: number }) => b.price - a.price);
    }
  }

  ngOnInit(): void {

  }

  showCars() {
    this.carService.getCars().subscribe((data) => {
      this.carInfo = data;
    });
  }

  toggleView(view: 'list' | 'grid') {
    console.log(view);
    if (view === 'list') {
      this.isListView = true;
    } else if (view === 'grid') {
      this.isListView = false;
    }
  }
}
