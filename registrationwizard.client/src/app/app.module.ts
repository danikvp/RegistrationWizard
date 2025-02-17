import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegistrationWizardComponent } from './components/registration-wizard/registration-wizard.component';
import { MaterialModule } from './material.module';
import { CustomSnackbarComponent } from './components/custom-snackbar/custom-snackbar.component';

@NgModule({
  declarations: [
    AppComponent,
    RegistrationWizardComponent,
    CustomSnackbarComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    MaterialModule
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
