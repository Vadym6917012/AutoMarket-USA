import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { MemberView } from 'src/app/models/admin/memberView';
import { Generation } from 'src/app/models/generation/generation';
import { AdminService } from 'src/app/services/admin.service';
import { GenerationService } from 'src/app/services/generation.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  members: MemberView[] = [];
  generations: Generation[] = [];

  memberToDelete: MemberView | undefined;
  generationToDelete: Generation| undefined;

  modalRef?: BsModalRef;

  constructor(private adminService: AdminService,
    private sharedService: SharedService,
    private modalService: BsModalService,
    private generationService: GenerationService) {}

  ngOnInit(): void {
    this.adminService.getMembers().subscribe({
      next: members => this.members = members
    });

    this.generationService.getGenerations().subscribe({
      next: generations => this.generations = generations
    });

  }

  deleteMember(id: string, template: TemplateRef<any>) {
    let member = this.findMember(id);
    if (member) {
      this.memberToDelete = member;
      this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
    }
  }

  deleteGeneration(id: number, template: TemplateRef<any>) {
    let generation = this.findGeneration(id);
    if (generation) {
      this.generationToDelete = generation;
      this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
    }
  }

  confirmMember() {
    if(this.memberToDelete) {
      this.adminService.deleteMember(this.memberToDelete.id).subscribe({
        next: _ => {
          this.sharedService.showNotification(true, 'Видалено', `${this.memberToDelete?.userName} видалено успішно!`);
          this.members = this.members.filter(x => x.id !== this.memberToDelete?.id);
          this.memberToDelete = undefined;
          this.modalRef?.hide();
        }
      })
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

  declineMember() {
    this.memberToDelete = undefined;
    this.modalRef?.hide();
  }

  declineGeneration() {
    this.generationToDelete = undefined;
    this.modalRef?.hide();
  }

  private findMember(id: string): MemberView | undefined {
    let member = this.members.find(x => x.id === id);
    if (member) {
      return member;
    }

    return undefined;
  }

  private findGeneration(id: number): Generation | undefined {
    let generation = this.generations.find(x => x.id === id);
    if (generation) {
      return generation;
    }

    return undefined;
  }
}
