import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { SharedService } from '../shared/shared.service';
import { Observable, map } from 'rxjs';
import { User } from '../models/account/user';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})

export class AdminGuard {
  constructor(private accountService: AccountService,
    private sharedService: SharedService,
    private router: Router) { }

  canActivate(): Observable<boolean> {
    return this.accountService.user$.pipe(
      map((user: User | null) => {

        if (user) {
          const decodedToken: any = jwtDecode(user.jwt);
          if (decodedToken.role.includes('Admin')) {
            return true;
          }
        }

        this.sharedService.showNotification(false, 'Admin Area', 'Leave now!');
        this.router.navigateByUrl('/');

        return false;
      })
    );
  }
}
