import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Make } from 'src/app/models/make/make';
import { MakeService } from 'src/app/services/make.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-makes',
  templateUrl: './makes.component.html',
  styleUrls: ['./makes.component.css']
})
export class MakesComponent implements OnInit {
  makes: Make[] = [];
  makeToDelete: Make | undefined;

  modalRef?: BsModalRef;

  constructor(private sharedService: SharedService,
    private modalService: BsModalService,
    private makeService: MakeService) {}

  ngOnInit(): void {
    this.makeService.getMakes().subscribe({
      next: make => this.makes = make
    });
  }

  deleteMake(id: number, template: TemplateRef<any>) {
    let make = this.findMake(id);
    if (make) {
      this.makeToDelete = make;
      this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
    }
  }

  confirmMake() {
    if(this.makeToDelete) {
      this.makeService.deleteMake(this.makeToDelete.id).subscribe({
        next: _ => {
          this.sharedService.showNotification(true, 'Видалено', `${this.makeToDelete?.name} видалено успішно!`);
          this.makes = this.makes.filter(x => x.id !== this.makeToDelete?.id);
          this.makeToDelete = undefined;
          this.modalRef?.hide();
        }
      })
    }
  }

  declineMake() {
    this.makeToDelete = undefined;
    this.modalRef?.hide();
  }

  private findMake(id: number): Make | undefined {
    let make = this.makes.find(x => x.id === id);
    if (make) {
      return make;
    }

    return undefined;
  }

}
