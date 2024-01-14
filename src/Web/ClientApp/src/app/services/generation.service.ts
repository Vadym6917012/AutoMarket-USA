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

  getGeneration(id: number) :Observable<Generation>{
    return this.http.get<Generation>(`${environment.apiUrl}/api/generation/${id}`);
  }

  getGenerationByModel(modelId: number): Observable<Generation[]>{
    return this.http.get<Generation[]>(`${environment.apiUrl}/api/generation/get-generation-by-model/${modelId}`);
  }
  
  addGenerationToModel(modelId: number, generation: Generation) :Observable<Generation>{
    const numericModelId = typeof modelId === 'string' ? parseInt(modelId, 10) : modelId;
    
    generation.id = 0;

    return this.http.post<Generation>(`${environment.apiUrl}/api/generation/add-generation-to-model?modelId=${modelId}`, generation);
  }

  deleteGeneration(id: number) :Observable<Generation> {
    return this.http.delete<Generation>(`${environment.apiUrl}/api/generation/${id}`);
  }
}
