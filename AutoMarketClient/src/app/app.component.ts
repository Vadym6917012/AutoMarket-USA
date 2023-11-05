import { AccountService } from './services/account.service';
import { Component, OnInit } from '@angular/core';
import { SharedService } from './shared/shared.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(private accountService: AccountService,
    private sharedService: SharedService) {}

  ngOnInit(): void {
    this.refreshUser();
  }

  private refreshUser() {
    const jwt = this.accountService.getJWT();
    if (jwt) {
      this.accountService.refreshUser(jwt).subscribe({
        next: _ => {},
        error: error => {
          this.accountService.logout();

          if (error.status === 401) {
            this.sharedService.showNotification(false, 'Акаунт заблоковано', error.error);
          }
        }
      })
    } else {
      this.accountService.refreshUser(null).subscribe();
    }
  } 
  title = 'AutoMarketClient';
}
