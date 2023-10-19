import { NotificationComponent } from './components/modals/notification/notification.component';
import { Injectable } from '@angular/core';
import { BsModalService, BsModalRef, ModalOptions } from 'ngx-bootstrap/modal';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  bsModalRef?: BsModalRef;

  constructor(private modalService: BsModalService) { }

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
}
