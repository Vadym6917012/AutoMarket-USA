import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MemberView } from '../models/admin/memberView';
import { environment } from 'src/environments/environment.development';
import { MemberAddEdit } from '../models/admin/memberAddEdit';
import { CarAdd } from '../models/car/car-add';
import { catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  getMembers() {
    return this.http.get<MemberView[]>(`${environment.apiUrl}/api/admin/get-members`);
  }

  getMember(id: string) {
    return this.http.get<MemberAddEdit>(`${environment.apiUrl}/api/admin/get-member/${id}`);
  }

  getApplicationRoles() {
    return this.http.get<string[]>(`${environment.apiUrl}/api/admin/get-application-roles`);
  }

  addEditMember(model: MemberAddEdit) {
    return this.http.post(`${environment.apiUrl}/api/admin/add-edit-member`, model);
  }

  deleteMember(id: string) {
    return this.http.delete(`${environment.apiUrl}/api/admin/delete-member/${id}`, {});
  }

  approveAdv(carId: number, isApproved: boolean) {
    const body = {carId, isApproved}
    return this.http.put<CarAdd>(`${environment.apiUrl}/api/admin/approve-advertisement?carId=${carId}&isApproved=${isApproved}`, {}).pipe(
      catchError(error => {
          console.error('Error approving/rejecting advertisement:', error);
          console.error('Server error details:', error.error); 
          return throwError(error);
      })
  );
  }
}
