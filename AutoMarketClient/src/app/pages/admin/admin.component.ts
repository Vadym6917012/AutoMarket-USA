import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { MemberView } from 'src/app/models/admin/memberView';
import { AdminService } from 'src/app/services/admin.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  members: MemberView[] = [];
  memberToDelete: MemberView | undefined;
  modalRef?: BsModalRef;

  constructor(private adminService: AdminService,
    private sharedService: SharedService,
    private modalService: BsModalService) {}

  ngOnInit(): void {
    this.adminService.getMembers().subscribe({
      next: members => this.members = members
    });
  }

  deleteMember(id: string, template: TemplateRef<any>) {
    let member = this.findMember(id);
    if (member) {
      this.memberToDelete = member;
      this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
    }
  }

  confirm() {
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

  decline() {
    this.memberToDelete = undefined;
    this.modalRef?.hide();
  }

  private findMember(id: string): MemberView | undefined {
    let member = this.members.find(x => x.id === id);
    if (member) {
      return member;
    }

    return undefined;
  }
}
