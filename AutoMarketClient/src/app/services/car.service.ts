import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Car } from '../models/car/car';
import { CarAdd } from '../models/car/car-add';
import { CarHomeFilter } from '../models/car/car-home-filter';
import { CarFilter } from '../models/car/car-filter';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  constructor(private http: HttpClient) { }

  getCars (): Observable<Car[]> {
    return this.http.get<Car[]>(`${environment.apiUrl}/api/car/get-cars`);
  }
  
  getUnverifiedCars(): Observable<Car[]> {
    return this.http.get<Car[]>(`${environment.apiUrl}/api/car/get-unverified-cars`);
  }

  getCar(carId: number) {
    return this.http.get<Car>(`${environment.apiUrl}/api/car/get-car/${carId}`);
  }
  
  getCarForUpdate(carId: number) {
    return this.http.get<CarAdd>(`${environment.apiUrl}/api/car/get-car-for-update/${carId}`);
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

  /*makeId: number;
    modelId: number;
    generationId: number;
    modificationId: number;
    bodyTypeId: number;
    gearBoxTypeId: number;
    driveTrainId: number;
    technicalConditionId: number;
    fuelTypeId: number;
    yearFrom: number;
    yearTo: number;
    mileageFrom: number;
    mileageTo: number;
    priceFrom: number;
    priceTo: number;*/

  carFilter(filter: CarFilter | any) :Observable<Car[]> {

    const params = new HttpParams()
    
    .set('makeId', filter.makeId.toString())
    .set('modelId', filter.modelId.toString())
    .set('generationId', filter.generationId.toString())
    .set('modificationId', filter.modificationId.toString())
    .set('bodyTypeId', filter.bodyTypeId.toString())
    .set('gearBoxTypeId', filter.gearBoxTypeId.toString())
    .set('driveTrainId', filter.driveTrainId.toString())
    .set('technicalConditionId', filter.technicalConditionId.toString())
    .set('fuelTypeId', filter.fuelTypeId.toString())
    .set('yearFrom', filter.yearFrom.toString())
    .set('yearTo', filter.yearTo.toString())
    .set('mileageFrom', filter.mileageFrom.toString())
    .set('mileageTo', filter.mileageTo.toString())
    .set('priceFrom', filter.priceFrom.toString())
    .set('priceTo', filter.priceTo.toString());

    return this.http.get<Car[]>(`${environment.apiUrl}/api/car/car-filter`, {params});
  }
}
