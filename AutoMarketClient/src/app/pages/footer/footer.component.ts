import { Component } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent {
  constructor(public accountService: AccountService) {

  }
  currentYear = new Date().getFullYear();
}
