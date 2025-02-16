import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Country } from '../models/country';
import { Province } from '../models/province';
import { RegistrationInfo } from '../models/registration-info';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  getCountryList(): Observable<Country[]> {
    return this.http.get<Country[]>('/api/Dictionaries/countries');
  }

  getProvinceList(countryId: number | undefined): Observable<Province[]> {
    const params = new HttpParams().set('CountryId', `${countryId == undefined ? -1 : countryId}`);
    return this.http.get<Province[]>('/api/Dictionaries/provinces', { params });
  }

  register(registrationInfo: RegistrationInfo): Observable<void> {
    console.log(registrationInfo);
    return this.http.post<void>('/api/registration', registrationInfo);
  }

}
