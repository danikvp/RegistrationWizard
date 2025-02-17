import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { Country } from '../../models/country';
import { Province } from '../../models/province';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { fieldsMismatchValidator } from '../../validation/form-group-fields-mismatch-validator';
import { FormGroupErrorStateMatcher } from '../../validation/form-group-error-state-matcher';
import { DataService } from '../../services/data.service';
import { Subject, tap, switchMap, takeUntil, finalize } from 'rxjs';
import { RegistrationInfo } from '../../models/registration-info';

@Component({
  selector: 'app-registration-wizard',
  standalone: false,
  templateUrl: './registration-wizard.component.html',
  styleUrl: './registration-wizard.component.scss'
})
export class RegistrationWizardComponent implements OnInit, OnDestroy {
  @Output() registered = new EventEmitter();

  countries: Country[] = [];
  provinces: Province[] = [];

  loading = false;
  showDebugInfo = false;


  firstFormGroup = new FormGroup({
    login: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required, Validators.email]
    }),
    password: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required, Validators.pattern('(?=.*[A-Z])(?=.*\\d).*')]
    }),
    passwordConfirm: new FormControl('', {
      nonNullable: true,
      validators: Validators.required
    }),
    agreed: new FormControl(false, {
      nonNullable: true,
      validators: Validators.requiredTrue
    })
  }, { validators: fieldsMismatchValidator('password', 'passwordConfirm') });


  countryControl = new FormControl<Country | null>(null, [Validators.required]);
  provinceControl = new FormControl<Province | null>(null, [Validators.required]);

  secondFormGroup = new FormGroup({
    country: this.countryControl,
    province: this.provinceControl,
  });

  matcher = new FormGroupErrorStateMatcher();

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

  private destroy$ = new Subject<void>();


  constructor(private _dataService: DataService) { }

  ngOnInit() {
    this.loadCountries();

    this.countryControl.valueChanges
      .pipe(
        tap(() => {
          this.provinceControl.setValue(null);
          this.provinces = [];
        }),
        switchMap((val) => {
          return this._dataService.getProvinceList(val!.id);
        }),
        takeUntil(this.destroy$)
      )
      .subscribe(provinces => {
        this.provinces = provinces;
        console.log(provinces);
      })
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  register() {
    if (!this.secondFormGroup.valid) return;

    this.loading = true;
    const { country, province } = this.secondFormGroup.value

    const registrationInfo: RegistrationInfo = <RegistrationInfo>{ ...this.firstFormGroup.value, countryId: country!.id, provinceId: province!.id };

    this._dataService.register(registrationInfo).pipe(
      finalize(() => {
        this.loading = false;
      })
    ).subscribe(() => {
      this.registered.emit();
    });
  }


  private loadCountries() {
    this._dataService.getCountryList().subscribe(countries => {
      this.countries = countries;
    });
  }


}
