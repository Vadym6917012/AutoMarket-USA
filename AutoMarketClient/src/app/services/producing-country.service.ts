import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProducingCountry } from '../models/producing-country/producing-country';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ProducingCountryService {

  constructor(private http: HttpClient) { }

  getProducingCountries (): Observable<ProducingCountry[]> {
    return this.http.get<ProducingCountry[]>(`${environment.apiUrl}/api/producingcountry`);
  }
}
