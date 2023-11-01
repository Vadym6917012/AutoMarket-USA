import { Injectable } from '@angular/core';
import { Model } from '../models/model/model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ModelService {

  constructor(private http: HttpClient) { }

  getModels (): Observable<Model[]> {
    return this.http.get<Model[]>(`${environment.apiUrl}/api/model`);
  }

  getModelsByMake(makeId: number): Observable<Model[]> {
    return this.http.get<Model[]>(`${environment.apiUrl}/api/model/get-model-by-make/${makeId}`);
  }

}
