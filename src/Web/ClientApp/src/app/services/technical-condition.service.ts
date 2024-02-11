import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TechnicalCondition } from '../models/technical-condition/technical-condition';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class TechnicalConditionService {

  constructor(private http: HttpClient) { }

  getTechnicalConditions() :Observable<TechnicalCondition[]> {
    return this.http.get<TechnicalCondition[]>(`${environment.apiUrl}/api/technicalcondition/get-technicalconditions`);
  }
}
