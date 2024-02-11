import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GearBox } from '../models/gearbox/gearbox';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class GearBoxTypeService {

  constructor(private http: HttpClient) { }

  getGearBoxes(): Observable<GearBox[]> {
    return this.http.get<GearBox[]>(`${environment.apiUrl}/api/gearboxtype/get-gearboxtypes`);
  }
}
