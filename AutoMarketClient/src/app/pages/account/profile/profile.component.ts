import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Car } from 'src/app/models/car/car';
import { CarService } from 'src/app/services/car.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  cars: Car[] = [];
  carToDelete: Car | undefined;
  modalRef?: BsModalRef;
  id: string | undefined;

  constructor(private activatedRoute: ActivatedRoute,
    private router: Router,
    private sharedService: SharedService,
    private modalService: BsModalService,
    private carService: CarService) { }

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      this.getCarsByUserId(id);
    } else {
      this.router.navigateByUrl("/");
    }
  }

  getCarsByUserId(id: string) {
    this.carService.getByUserId(id).subscribe(data => {
      this.cars = data;
    })
  }

  deleteCar(id: number, template: TemplateRef<any>) {
    let car = this.findCar(id);
    if (car) {
      this.carToDelete = car;
      this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
    }
  }

  confirm() {
    if(this.carToDelete) {
      this.carService.deleteCar(this.carToDelete.id).subscribe({
        next: _ => {
          this.sharedService.showNotification(true, 'Видалено', `Оголошення ${this.carToDelete?.id} видалено успішно!`);
          this.cars = this.cars.filter(x => x.id !== this.carToDelete?.id);
          this.carToDelete = undefined;
          this.modalRef?.hide();
        }
      })
    }
  }

  decline() {
    this.carToDelete = undefined;
    this.modalRef?.hide();
  }

  private findCar(id: number): Car | undefined {
    let car = this.cars.find(x => x.id === id);
    if (car) {
      return car;
    }

    return undefined;
  }

}
