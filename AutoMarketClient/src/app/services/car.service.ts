import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Car } from '../models/car/car';
import { CarAdd } from '../models/car/car-add';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  constructor(private http: HttpClient) { }

  getCars (): Observable<Car[]> {
    return this.http.get<Car[]>(`${environment.apiUrl}/api/car`)
  }

  addCar (model: CarAdd | any, images: File[]) {
    const formData = new FormData();

    for (const key in model) {
      if (model.hasOwnProperty(key)) {
        formData.append(key, model[key]);
      }
    }

    for (let i = 0; i < images.length; i++) {
      formData.append(`images`, images[i]);
    }

    return this.http.post<CarAdd>(`${environment.apiUrl}/api/car/create-with-images`, formData)
  }
}
