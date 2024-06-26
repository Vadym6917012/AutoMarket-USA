import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Make } from '../models/make/make';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class MakeService {

  constructor(private http: HttpClient) { }

  getMakes (): Observable<Make[]> {
    return this.http.get<Make[]>(`${environment.apiUrl}/api/make/get-makes`);
  }

  getMakeByCountry(producingCountryId: number): Observable<Make[]> {
    return this.http.get<Make[]>(`${environment.apiUrl}/api/make/get-make-by-country/${producingCountryId}`);
  }

  deleteMake (id: number) {
    return this.http.delete<Make>(`${environment.apiUrl}/api/make/delete-make/${id}`, {});
  }
}
