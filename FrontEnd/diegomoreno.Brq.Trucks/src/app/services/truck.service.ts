import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GetTruckResponseModel } from '../models/GetTruckResponseModel';
import { TruckModel } from '../models/TruckModel';

@Injectable({
  providedIn: 'root'
})
export class TruckService {
  baseURL = environment.serviceUrl + '/trucks';

  constructor(private http: HttpClient) { }

  getAll(): Observable<GetTruckResponseModel[]>{
    var response = this.http.get<GetTruckResponseModel[]>(this.baseURL);
    return response;
  }
  getById(id: string): Observable<GetTruckResponseModel>{
    var response =
      this.http
        .get<GetTruckResponseModel>(`${this.baseURL}/${id}`)
        .pipe(take(1));
    return response;
  }
  post(truck: TruckModel): Observable<TruckModel>{
    var response = this.http
      .post<TruckModel>(this.baseURL, truck)
      .pipe(take(1));
    return response;
  }
  put(truck: TruckModel, idTruck: string): Observable<TruckModel>{
    var response = this.http
      .put<TruckModel>(`${this.baseURL}/${idTruck}`, truck)
      .pipe(take(1));
    return response;
  }
  delete(id: string): Observable<any>{
    var response =
      this.http
        .delete(`${this.baseURL}/${id}`)
        .pipe(take(1));
    return response;
  }
}
