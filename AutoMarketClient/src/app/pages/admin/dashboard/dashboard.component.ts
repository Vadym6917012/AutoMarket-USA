import { Component, OnInit } from '@angular/core';
import { Car } from 'src/app/models/car/car';
import { AdminService } from 'src/app/services/admin.service';
import { CarService } from 'src/app/services/car.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  cars: Car[] = [];

  constructor(private carService: CarService,
     private adminService: AdminService,
     private sharedService: SharedService) {}

  ngOnInit(): void {
    this.carService.getUnverifiedCars().subscribe({
      next: cars => this.cars = cars
    });
  }

  approve(carId: number, isApproved: boolean) {
    this.adminService.approveAdv(carId, isApproved).subscribe({
      next: _ => {
        if (isApproved) {
          this.sharedService.showNotification(true, 'Підтвердженно', `Оголошення підтвердженно успішно!`);
        } else {
          this.sharedService.showNotification(false, 'Відхилено', `Оголошення відхилено успішно!`);
        }
        this.cars = this.cars.filter(x => x.id !== carId);
      },
      error: error => {
        console.error('Error approving/rejecting advertisement:', error);
        this.sharedService.showNotification(false, 'Помилка', 'Виникла помилка при обробці оголошення.');
      }
    });
  }

}
