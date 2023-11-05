import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BodyType } from '../models/body-type/body-type';
import { environment } from 'src/environments/environment.development';
import { FuelType } from '../models/fuel-type/fuel-type';

@Injectable({
  providedIn: 'root'
})
export class FuelTypeService {

  constructor(private http: HttpClient) { }

  getFuelTypes() :Observable<FuelType[]> {
    return this.http.get<FuelType[]>(`${environment.apiUrl}/api/fueltype`);
  }
}
