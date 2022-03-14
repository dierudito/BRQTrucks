import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { TruckModel } from '../models/Truck';

@Injectable({
  providedIn: 'root',
})
export class TruckService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<TruckModel[]>{
    return this.http.get<TruckModel[]>(environment.serviceUrl);
  }
  getById(idTruck: any): Observable<TruckModel>{
    return this.http.get<TruckModel>(`${environment.serviceUrl}/${idTruck}`);
  }

  delete(idTruck: any){
    this.http.delete(`${environment.serviceUrl}/${idTruck}`);
  }
}
