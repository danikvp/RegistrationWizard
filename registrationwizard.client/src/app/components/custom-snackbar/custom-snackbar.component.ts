import { Component, Inject } from '@angular/core';
import { MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';

@Component({
  selector: 'app-custom-snackbar',
  standalone: false,
  templateUrl: './custom-snackbar.component.html',
  styleUrl: './custom-snackbar.component.scss'
})
export class CustomSnackbarComponent {
  constructor(@Inject(MAT_SNACK_BAR_DATA) public data: unknown) {}
}
