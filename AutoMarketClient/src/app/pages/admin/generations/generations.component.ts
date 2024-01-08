import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Generation } from 'src/app/models/generation/generation';
import { GenerationService } from 'src/app/services/generation.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-generations',
  templateUrl: './generations.component.html',
  styleUrls: ['./generations.component.css']
})
export class GenerationsComponent implements OnInit {

  generations: Generation[] = [];
  generationToDelete: Generation| undefined;

  modalRef?: BsModalRef;

  constructor(private sharedService: SharedService,
    private modalService: BsModalService,
    private generationService: GenerationService) {}

  ngOnInit(): void {
    this.generationService.getGenerations().subscribe({
      next: generations => this.generations = generations
    });
  }

  deleteGeneration(id: number, template: TemplateRef<any>) {
    let generation = this.findGeneration(id);
    if (generation) {
      this.generationToDelete = generation;
      this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
    }
  }

  confirmGeneration() {
    if(this.generationToDelete) {
      this.generationService.deleteGeneration(this.generationToDelete.id).subscribe({
        next: _ => {
          this.sharedService.showNotification(true, 'Видалено', `${this.generationToDelete?.name} видалено успішно!`);
          this.generations = this.generations.filter(x => x.id !== this.generationToDelete?.id);
          this.generationToDelete = undefined;
          this.modalRef?.hide();
        }
      })
    }
  }

  declineGeneration() {
    this.generationToDelete = undefined;
    this.modalRef?.hide();
  }

  private findGeneration(id: number): Generation | undefined {
    let generation = this.generations.find(x => x.id === id);
    if (generation) {
      return generation;
    }

    return undefined;
  }

}
