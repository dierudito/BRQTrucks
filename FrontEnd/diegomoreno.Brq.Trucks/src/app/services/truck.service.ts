import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TruckModel } from '../models/TruckModel';

@Injectable({
  providedIn: 'root'
})
export class TruckService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<TruckModel[]>{
    var response = this.http.get<TruckModel[]>(environment.serviceUrl);
    return response;
  }
  getById(id: string): Observable<TruckModel>{
    var response =
      this.http
        .get<TruckModel>(`${environment.serviceUrl}/${id}`)
        .pipe(take(1));
    return response;
  }
  post(truck: TruckModel): Observable<TruckModel>{
    var response = this.http
      .post<TruckModel>(environment.serviceUrl, truck)
      .pipe(take(1));
    return response;
  }
  put(truck: TruckModel): Observable<TruckModel>{
    var response = this.http
      .put<TruckModel>(environment.serviceUrl, truck)
      .pipe(take(1));
    return response;
  }
  delete(id: string): Observable<any>{
    var response =
      this.http
        .delete(`${environment.serviceUrl}/${id}`)
        .pipe(take(1));
    return response;
  }
}
