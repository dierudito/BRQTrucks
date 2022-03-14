import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SeriesModel } from 'src/app/models/SeriesModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SeriesService {
  baseURL = environment.serviceUrl + '/series';

  constructor(private http: HttpClient) { }

  getAll(): Observable<SeriesModel[]>{
    return this.http.get<SeriesModel[]>(this.baseURL);
  }
  getById(idSeries: any): Observable<SeriesModel>{
    return this.http.get<SeriesModel>(`${this.baseURL}/${idSeries}`);
  }
}
