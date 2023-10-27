import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Car } from '../models/car/car';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  constructor(private http: HttpClient) { }

  getCars (): Observable<Car[]> {
    return this.http.get<Car[]>(`${environment.apiUrl}/api/car`)
  }
}
