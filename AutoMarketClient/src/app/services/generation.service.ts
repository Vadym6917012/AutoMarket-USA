import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Generation } from '../models/generation/generation';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class GenerationService {

  constructor(private http: HttpClient) { }

  getGenerations() :Observable<Generation[]>{
    return this.http.get<Generation[]>(`${environment.apiUrl}/api/generation`);
  }

  getGenerationByModel(modelId: number): Observable<Generation[]>{
    return this.http.get<Generation[]>(`${environment.apiUrl}/api/generation/get-generation-by-model/${modelId}`);
  }
}
