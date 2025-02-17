import { Injectable } from '@angular/core';
import { catchError, map, Observable, throwError } from 'rxjs';
import { Country } from '../models/country';
import { Province } from '../models/province';
import { RegistrationInfo } from '../models/registration-info';
import { ajax, AjaxConfig, AjaxError, AjaxTimeoutError } from 'rxjs/ajax';
import { ErrorHandlingService } from './error-handling.service';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private errorHandlingService: ErrorHandlingService) { }

  getCountryList(): Observable<Country[]> {
    return this.ajaxRequest<Country[]>({
      method: 'GET',
      url: '/api/Dictionaries/countries',

    });
  }

  getProvinceList(countryId: number): Observable<Province[]> {

    const queryParams: Record<string, number> = {
      CountryId: countryId
    };

    return this.ajaxRequest<Province[]>({
      method: 'GET',
      url: '/api/Dictionaries/provinces',
      queryParams,
    });
  }

  register(registrationInfo: RegistrationInfo): Observable<number> {
    return this.ajaxRequest<number>({
      method: 'POST',
      url: '/api/registration',
      body: registrationInfo
    });
  }


  private ajaxRequest<T>(config: AjaxConfig): Observable<T> {
    return ajax<T>(config).pipe(
      map(responce => responce.response),
      catchError(error => {
        var ajaxError = error as AjaxError;

        if (ajaxError) {
          if (ajaxError.status == 500)
            this.errorHandlingService.processError("Internal server error.");
          else
            this.errorHandlingService.processError(ajaxError.response || ajaxError.message || ajaxError);
        }
        else
          this.errorHandlingService.processError(error);

        return throwError(() => error);
      })
    );
  }

}
