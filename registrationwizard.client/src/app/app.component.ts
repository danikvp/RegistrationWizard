import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';

import { DataService } from './services/data.service';
import { Country } from './models/country';
import { Province } from './models/province';
import { switchMap, takeUntil, tap } from 'rxjs/operators';
import { RegistrationInfo } from './models/registration-info';
import { Subject } from 'rxjs';
import { fieldsMismatchValidator } from './validation/form-group-fields-mismatch-validator';
import { FormGroupErrorStateMatcher } from './validation/form-group-error-state-matcher';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.scss'
})
export class AppComponent {
  registrationCompleted = false;

  registered() {
    this.registrationCompleted = true;
  }

  title = 'registrationwizard.client';
}

