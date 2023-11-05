import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Car } from '../models/car/car';
import { CarAdd } from '../models/car/car-add';
import { CarHomeFilter } from '../models/car/car-home-filter';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  constructor(private http: HttpClient) { }

  getCars (): Observable<Car[]> {
    return this.http.get<Car[]>(`${environment.apiUrl}/api/car/get-cars`);
  }

  getCar(carId: number) {
    return this.http.get<Car>(`${environment.apiUrl}/api/car/get-car/${carId}`);
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

  deleteCar(carId: number) {
    return this.http.delete<Car>(`${environment.apiUrl}/api/car/delete-car/${carId}`, {});
  }

  updateCar(car: CarAdd) {
    return this.http.put<CarAdd>(`${environment.apiUrl}/api/car/update-car`, car);
  }
  
  getByCount(count: number) :Observable<Car[]> {
    return this.http.get<Car[]>(`${environment.apiUrl}/api/car/get-cars-by-count/${count}`);
  }
  
  getByUserId(id: string) :Observable<Car[]> {
    return this.http.get<Car[]>(`${environment.apiUrl}/api/car/get-car-by-userid/${id}`);
  }

  homeFilter(filter: CarHomeFilter | any) :Observable<Car[]> {

    const params = new HttpParams()
    .set('makeId', filter.makeId.toString())
    .set('modelId', filter.modelId.toString())
    .set('priceFrom', filter.priceFrom.toString())
    .set('priceTo', filter.priceTo.toString());

    return this.http.get<Car[]>(`${environment.apiUrl}/api/car/car-home-filter`, {params});
  }
}
