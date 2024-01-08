import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Modification } from 'src/app/models/modification/modification';
import { ModificationService } from 'src/app/services/modification.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-modifications',
  templateUrl: './modifications.component.html',
  styleUrls: ['./modifications.component.css']
})
export class ModificationsComponent implements OnInit {
  modifications: Modification[] = [];
  modificationToDelete: Modification | undefined;

  modalRef?: BsModalRef;

  constructor(private sharedService: SharedService,
    private modalService: BsModalService,
    private modificationService: ModificationService) {}

  ngOnInit(): void {
    this.modificationService.getModifications().subscribe({
      next: modification => this.modifications = modification
    });
  }

  deleteModification(id: number, template: TemplateRef<any>) {
    let modification = this.findModification(id);
    if (modification) {
      this.modificationToDelete = modification;
      this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
    }
  }

  confirmModification() {
    if(this.modificationToDelete) {
      this.modificationService.deleteModification(this.modificationToDelete.id).subscribe({
        next: _ => {
          this.sharedService.showNotification(true, 'Видалено', `${this.modificationToDelete?.name} видалено успішно!`);
          this.modifications = this.modifications.filter(x => x.id !== this.modificationToDelete?.id);
          this.modificationToDelete = undefined;
          this.modalRef?.hide();
        }
      })
    }
  }

  declineModification() {
    this.modificationToDelete = undefined;
    this.modalRef?.hide();
  }

  private findModification(id: number): Modification | undefined {
    let modification = this.modifications.find(x => x.id === id);
    if (modification) {
      return modification;
    }

    return undefined;
  }
}
