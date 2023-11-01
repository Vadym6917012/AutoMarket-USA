import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Image } from '../models/photos/image';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private http: HttpClient) { }

  getPhotosByCarId(carId: number) : Observable<Image[]>{

    return this.http.get<Image[]>(`${environment.apiUrl}/api/images/${carId}`);
  }
}
