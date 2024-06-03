import { NotificationComponent } from './components/modals/notification/notification.component';
import { Injectable } from '@angular/core';
import { AbstractControl } from '@angular/forms';
import { BsModalService, BsModalRef, ModalOptions } from 'ngx-bootstrap/modal';
import { VinCheckNotificationComponent } from './components/modals/vin-check-notification/vin-check-notification.component';
import { VinCheckResponce } from '../models/car/VinCheckResponse';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  bsModalRef?: BsModalRef;

  constructor(private modalService: BsModalService) { }

  checkingPasswords(registerForm: AbstractControl) {
    const password: string = registerForm.get('password')?.value;
    const confirmPassword: string = registerForm.get('confirmPassword')?.value;
    if (password !== confirmPassword) 
    {
      registerForm.get('confirmPassword')?.setErrors({NoPasswordMatch: true});
    }
  }

  showNotification(isSuccess: boolean, title: string, message: string) {
    const initialState: ModalOptions = {
      initialState: {
        isSuccess,
        title,
        message
      }
    };

    this.bsModalRef = this.modalService.show(NotificationComponent, initialState);
  }

  showVinNotification(response: VinCheckResponce) {
    const initialState: ModalOptions = {
      initialState: {
        isSuccess: response.isFound,
        title: response.vin,
        message: response.message,
        data: response
      }
    };

    this.bsModalRef = this.modalService.show(VinCheckNotificationComponent, initialState);
  }
}
