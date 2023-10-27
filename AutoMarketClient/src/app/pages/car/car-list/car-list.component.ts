import { Component, OnInit } from '@angular/core';
import { Car } from 'src/app/models/car/car';
import { Image } from 'src/app/models/image/image';
import { CarService } from 'src/app/services/car.service';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css']
})
export class CarListComponent implements OnInit {
  carInfo: Car[];
  carImages: { [carId: number]: Image[] } = {};

  constructor(private carService: CarService,
    private imageService: ImageService) {
      this.carInfo = [];
    }

  ngOnInit(): void {
   this.showCars();
  }

  showCars() {
    this.carService.getCars().subscribe((data) => {
      this.carInfo = data;

      this.carInfo.forEach((carInfo) => {
        this.imageService.getPhotosByCarId(carInfo.id).subscribe((images) =>{
          this.carImages[carInfo.id] = images;
        })
      })
    });
  }
}
