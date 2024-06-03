import { Component, Input } from '@angular/core';
import { BsModalRef, ModalOptions } from 'ngx-bootstrap/modal';
import { VinCheckResponce } from 'src/app/models/car/VinCheckResponse';

@Component({
  selector: 'app-vin-check-notification',
  templateUrl: './vin-check-notification.component.html',
  styleUrls: ['./vin-check-notification.component.css']
})
export class VinCheckNotificationComponent {
  data: VinCheckResponce | null = null;

  constructor(public bsModalRef: BsModalRef) { }
}
