import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Register } from '../models/account/register';
import { environment } from 'src/environments/environment.development';
import { Login } from '../models/account/login';
import { User } from '../models/account/user';
import { ReplaySubject, map, of } from 'rxjs';
import { Router } from '@angular/router';
import { ConfirmEmail } from '../models/account/confirmEmail';
import { ResetPassword } from '../models/account/resetPassword';
import { SharedService } from '../shared/shared.service';
import { MemberAddEdit } from '../models/admin/memberAddEdit';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private userSource = new ReplaySubject<User | null>(1);
  user$ = this.userSource.asObservable();

  constructor(private http: HttpClient, private router: Router, private sharedService: SharedService) { }

  refreshToken = async () => {
    this.http.post<User>(`${environment.apiUrl}/api/account/refresh-token`, {}, {withCredentials: true})
    .subscribe({
      next: (user: User) => {
        if (user) {
          this.setUser(user);
        }
      }, error: error => {
        this.sharedService.showNotification(false, 'Error', error.error);
        this.logout();
      }
    })
  }

  refreshUser(jwt: string | null) {
    if (jwt === null) {
      this.userSource.next(null);
      return of(undefined);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', 'Bearer ' + jwt);

    return this.http.get<User>(`${environment.apiUrl}/api/account/refresh-page`, {headers, withCredentials: true}).pipe(
      map((user: User) => {
        this.setUser(user);
      })
    );
  }

  login(model: Login) {
    return this.http.post<User>(`${environment.apiUrl}/api/account/login`, model).pipe(
      map((user: User) => {
        if (user) {
          this.setUser(user);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem(environment.userKey);
    this.userSource.next(null);
    this.router.navigateByUrl('/');
  }

  register(model: Register) {
    return this.http.post(`${environment.apiUrl}/api/account/register`, model);
  }

  confirmEmail(model: ConfirmEmail) {
    return this.http.put(`${environment.apiUrl}/api/account/confirm-email`, model);
  }

  resendConfirmationLink(email: string) {
    return this.http.post(`${environment.apiUrl}/api/account/resend-email-confirmation-link/${email}`, {});
  }

  forgotUsernameOrPassword(email: string) {
    return this.http.post(`${environment.apiUrl}/api/account/forgot-username-or-password/${email}`, {});
  }

  getMember(id: string) {
    return this.http.get<MemberAddEdit>(`${environment.apiUrl}/api/account/get-member/${id}`);
  }

  addEditMember(model: MemberAddEdit) {
    return this.http.post(`${environment.apiUrl}/api/account/edit-member`, model);
  }

  resetPassword(model: ResetPassword) {
    return this.http.put(`${environment.apiUrl}/api/account/reset-password`, model);
  }

  getJWT() {
    const key = localStorage.getItem(environment.userKey);
    if (key) {
      const user: User = JSON.parse(key);
      return user.jwt;
    } else {
      return null;
    }
  }

  private setUser(user: User) {
    localStorage.setItem(environment.userKey, JSON.stringify(user));
    this.userSource.next(user);
  }
}
