import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function fieldsMismatchValidator(firstControlName: string, secondControlName: string): ValidatorFn {
  return (formGroup: AbstractControl): ValidationErrors | null => {
    const firstControl = formGroup.get(firstControlName)!;
    const secondConfirmControl = formGroup.get(secondControlName)!;

    const areValuesDifferent = firstControl!.value !== secondConfirmControl!.value;

    if (
      secondConfirmControl.errors &&
      !secondConfirmControl.errors['controlsValuesMismatch']
    ) {
      return null;
    }


    if (areValuesDifferent) {
      secondConfirmControl.setErrors({ controlsValuesMismatch: true });
    }
    else
      secondConfirmControl.setErrors(null);


    return areValuesDifferent ? { 'controlsValuesMismatch': true } : null;
  }
}
