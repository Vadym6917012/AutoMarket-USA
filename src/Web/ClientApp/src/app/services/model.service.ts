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

  getModel(id: number) :Observable<Model>{
    return this.http.get<Model>(`${environment.apiUrl}/api/model/get-model/${id}`);
  }

  getModels (): Observable<Model[]> {
    return this.http.get<Model[]>(`${environment.apiUrl}/api/model/get-models`);
  }
  
  addModel(model: Model) :Observable<Model>{    
    model.id = 0;

    return this.http.post<Model>(`${environment.apiUrl}/api/model/create-model`, model);
  }

  getModelsByMake(makeId: number): Observable<Model[]> {
    return this.http.get<Model[]>(`${environment.apiUrl}/api/model/get-model-by-make/${makeId}`);
  }

  deleteModel (id: number) {
    return this.http.delete<Model>(`${environment.apiUrl}/api/model/delete-model/${id}`, {});
  }
}
