import { AccountService } from './services/account.service';
import { Component, OnInit } from '@angular/core';
import { SharedService } from './shared/shared.service';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  isAdminComponent: boolean = false;

  constructor(private accountService: AccountService, private router: Router, private route: ActivatedRoute,
    private sharedService: SharedService) {
      this.router.events.pipe(
        filter((event) => event instanceof NavigationEnd)
      ).subscribe(() => {
        const currentPath = this.router.routerState.snapshot.url;
        console.log('Current Path:', currentPath);
  
        this.isAdminComponent = currentPath.includes('/admin');
        console.log('isAdminComponent:', this.isAdminComponent);
      });
    }

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
