import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { MemberAddEdit } from 'src/app/models/admin/memberAddEdit';
import { Car } from 'src/app/models/car/car';
import { AccountService } from 'src/app/services/account.service';
import { AdminService } from 'src/app/services/admin.service';
import { CarService } from 'src/app/services/car.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  memberForm: FormGroup = new FormGroup({});
  cars: Car[] = [];
  carToDelete: Car | undefined;
  modalRef?: BsModalRef;
  id: string | undefined;
  submitted = false;
  formInitialized = false;
  errorMessages: string[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private adminService: AdminService,
    private accountService: AccountService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private sharedService: SharedService,
    private modalService: BsModalService,
    private carService: CarService) { }

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      this.getMember(id!);
      this.getCarsByUserId(id);
    } else {
      this.router.navigateByUrl("/");
    }
  }

  getMember(id: string) {
    this.accountService.getMember(id).subscribe({
      next: member => {
        this.initializeForm(member);
      }
    })
  }

  initializeForm(member: MemberAddEdit) {
      console.log(member);
      this.memberForm = this.formBuilder.group({
        id: [member.id],
        firstName: [member.firstName, Validators.required],
        lastName: [member.lastName, Validators.required],
        userName: [member.userName, Validators.required],
        phoneNumber: [member.phoneNumber],
        password: [''],
        roles: [member.roles, Validators.required]
      });
      this.formInitialized = true;
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

  submit() {
    this.submitted = true;
    this.errorMessages = [];

    if (this.memberForm.valid) {
      this.accountService.addEditMember(this.memberForm.value).subscribe({
        next: (response: any) => {
          this.sharedService.showNotification(true, response.value.titile, response.value.message);
          this.router.navigateByUrl('/');
        },
        error: error => {
          if (error.error.errors) {
            this.errorMessages = error.error.errors;
          } else {
            this.errorMessages.push(error.error);
          }
        }
      })
    }
  }

  passwordOnChange() {
      if (this.memberForm.get('password')?.value) {
        this.memberForm.controls['password'].setValidators([Validators.required, Validators.minLength(6), Validators.maxLength(15)]);
      } else {
        this.memberForm.get('password')?.clearValidators();
      }

      this.memberForm.controls['password'].updateValueAndValidity();
  }

  private findCar(id: number): Car | undefined {
    let car = this.cars.find(x => x.id === id);
    if (car) {
      return car;
    }

    return undefined;
  }
}
