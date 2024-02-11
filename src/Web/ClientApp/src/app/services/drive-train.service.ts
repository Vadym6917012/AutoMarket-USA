import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DriveTrain } from '../models/drive-train/drive-train';
import { environment } from 'src/environments/environment.development';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DriveTrainService {

  constructor(private http: HttpClient) { }

  getDriveTrains() :Observable<DriveTrain[]> {
    return this.http.get<DriveTrain[]>(`${environment.apiUrl}/api/drivetrain/get-drivetrains`);
  }
}
