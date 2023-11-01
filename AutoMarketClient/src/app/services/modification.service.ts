import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Modification } from '../models/modification/modification';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ModificationService {

  constructor(private http: HttpClient) { }

  getModifications (): Observable<Modification[]> {
    return this.http.get<Modification[]>(`${environment.apiUrl}/api/modification`);
  }
  
  getModificationsByModel(modelId: number): Observable<Modification[]> {
    return this.http.get<Modification[]>(`${environment.apiUrl}/api/modification/get-modification-by-model/${modelId}`)
  }
}
