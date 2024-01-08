import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Model } from 'src/app/models/model/model';
import { ModelService } from 'src/app/services/model.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-models',
  templateUrl: './models.component.html',
  styleUrls: ['./models.component.css']
})
export class ModelsComponent implements OnInit {
  models: Model[] = [];
  modelToDelete: Model | undefined;

  modalRef?: BsModalRef;

  constructor(private sharedService: SharedService,
    private modalService: BsModalService,
    private modelService: ModelService) {}

  ngOnInit(): void {
    this.modelService.getModels().subscribe({
      next: model => this.models = model
    });
  }

  deleteModel(id: number, template: TemplateRef<any>) {
    let model = this.findModel(id);
    if (model) {
      this.modelToDelete = model;
      this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
    }
  }

  confirmModel() {
    if(this.modelToDelete) {
      this.modelService.deleteModel(this.modelToDelete.id).subscribe({
        next: _ => {
          this.sharedService.showNotification(true, 'Видалено', `${this.modelToDelete?.name} видалено успішно!`);
          this.models = this.models.filter(x => x.id !== this.modelToDelete?.id);
          this.modelToDelete = undefined;
          this.modalRef?.hide();
        }
      })
    }
  }

  declineModel() {
    this.modelToDelete = undefined;
    this.modalRef?.hide();
  }

  private findModel(id: number): Model | undefined {
    let model = this.models.find(x => x.id === id);
    if (model) {
      return model;
    }

    return undefined;
  }
}
