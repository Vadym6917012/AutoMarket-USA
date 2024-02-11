import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BodyType } from '../models/body-type/body-type';
import { environment } from 'src/environments/environment.development';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BodyTypeService {

  constructor(private http: HttpClient) { }

  getBodyTypes() :Observable<BodyType[]> {
    return this.http.get<BodyType[]>(`${environment.apiUrl}/api/bodytype/get-bodytypes`);
  }
}
