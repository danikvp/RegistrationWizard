import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CustomSnackbarComponent } from '../components/custom-snackbar/custom-snackbar.component';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlingService {
  constructor(private snackBar: MatSnackBar) { }

  public processError(messageObj: unknown) {
    this.snackBar.openFromComponent(CustomSnackbarComponent, { data: messageObj, duration: 5000 });
  }
}
