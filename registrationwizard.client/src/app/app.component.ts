import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, FormGroupDirective, NgForm, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';

import { DataService } from './services/data.service';
import { Country } from './models/country';
import { Province } from './models/province';
import { switchMap, takeUntil, tap } from 'rxjs/operators';
import { RegistrationInfo } from './models/registration-info';
import { Subject } from 'rxjs';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit, OnDestroy {
  public countries: Country[] = [];
  public provinces: Province[] = [];

  loaded = false;

  destroy$ = new Subject<void>();


  formGroupValidator: ValidatorFn = (
    formGroup: AbstractControl
  ): ValidationErrors | null => {

    const passwordControl = formGroup.get('password')!;
    const passwordConfirmControl = formGroup.get('passwordConfirm')!;

    const areValuesDifferent = passwordControl!.value !== passwordConfirmControl!.value;

    if (
      passwordConfirmControl.errors &&
      !passwordConfirmControl.errors['passwordMismatch']
    ) {
      return null;
    }


    if (areValuesDifferent) {
      passwordConfirmControl.setErrors({ passwordMismatch: true });
    }
    else
      passwordConfirmControl.setErrors(null);


    return areValuesDifferent ? { 'passwordMismatch': true } : null;
  };

  firstFormGroup = new FormGroup({
    login: new FormControl('', { nonNullable: true, validators: [Validators.required, Validators.email] }),
    password: new FormControl('', { nonNullable: true, validators: [Validators.required, Validators.pattern('(?=.*[A-Z])(?=.*\\d).*')] }),
    passwordConfirm: new FormControl('', { nonNullable: true, validators: Validators.required }),
    agreed: new FormControl(false, { nonNullable: true, validators: Validators.requiredTrue })
  }, { validators: this.formGroupValidator });


  countryControl = new FormControl<Country | null>(null, [Validators.required]);
  provinceControl = new FormControl<Province | null>(null, [Validators.required]);

  secondFormGroup = new FormGroup({
    country: this.countryControl,
    province: this.provinceControl,
  });


  matcher = new MyErrorStateMatcher();

  get controls2() {
    return this.secondFormGroup.controls;
  }

  get controls() {
    return this.firstFormGroup.controls;
  }

  get provincePlaceHolder() {
    return !this.secondFormGroup.controls.country.value ? 'Please select Country first' : 'Please select Province'
  }


  get loginValidEmailError(): boolean {
    const loginControl = this.firstFormGroup.controls.login;
    return loginControl.hasError('email');
  }
  get loginRequiredError(): boolean {
    const loginControl = this.firstFormGroup.controls.login;
    return loginControl.hasError('required');
  }

  constructor(
    private http: HttpClient,
    private _dataService: DataService,

  ) { }



  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  ngOnInit() {
    this.loadCountries();

    this.countryControl.valueChanges
      .pipe(
        tap(() => {
          this.provinceControl.setValue(null);
          this.provinces = [];
        }),
        switchMap((val) => {
          return this._dataService.getProvinceList(val?.id);
        }),
        takeUntil(this.destroy$)
      )
      .subscribe(provinces => {
        this.provinces = provinces;
        console.log(provinces);
      })
  }

  private loadCountries() {
    this._dataService.getCountryList().subscribe(countries => {
      this.countries = countries;
    });
  }

  register() {
    if (!this.secondFormGroup.valid) return;

    this.loaded = true;
    const { country, province } = this.secondFormGroup.value

    const registrationInfo: RegistrationInfo = <RegistrationInfo>{ ...this.firstFormGroup.value, countryId: country!.id, provinceId: province!.id };

    this._dataService.register(registrationInfo).subscribe(() => {
      this.loaded = false;
    });
  }

  title = 'registrationwizard.client';
}

