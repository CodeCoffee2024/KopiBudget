import { Injectable } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { toCamelCase } from '../../ui/shared/utils/string.util';

export interface ServerError {
  name: string;
  description: string;
}

@Injectable({
  providedIn: 'root'
})
export class FormErrorService {
  injectServerErrorControl(form: FormGroup): void {
    if (!form.contains('serverError')) {
      form.addControl('serverError', new FormControl(''));
    }
  }

  /**
   * Accepts either a single ServerError or an array
   */
  setServerErrors(
    formGroup: FormGroup,
    errors: ServerError | ServerError[] | null | undefined
  ): void {
    if (!errors) return;
    const normalizedErrors = Array.isArray(errors) ? Array.isArray(errors[0]) ? errors[0] : errors : [errors];

    this.markFormGroupDirty(formGroup);
    this.clearServerErrorOnChange(formGroup);

    const globalErrors: Record<string, string[]> = {};

    normalizedErrors.forEach(({ name, description }) => {
      const controlKey = toCamelCase(name);
      if (formGroup.controls[controlKey]) {
        const control = formGroup.controls[controlKey];
        const currentErrors = control.errors || {};
        const serverErrors = currentErrors['serverError'] || [];

        control.setErrors({
          ...currentErrors,
          serverError: [...serverErrors, description],
        });
      } else {
        if (!globalErrors[name]) {
          globalErrors[name] = [];
        }
        globalErrors[name].push(description);
      }
    });

    if (Object.keys(globalErrors).length > 0) {
      formGroup.setErrors({ serverError: globalErrors });
    }
  }

  private markFormGroupDirty(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach((field) => {
      const control = formGroup.get(field);
      control?.markAsDirty({ onlySelf: true });

      if (control instanceof FormGroup) {
        this.markFormGroupDirty(control);
      }
    });
  }

  clearServerErrorOnChange(form: FormGroup): void {
    if (form.errors?.['serverError']) {
      const { serverError, ...rest } = form.errors;
      form.setErrors(Object.keys(rest).length > 0 ? rest : null);
    }

    Object.keys(form.controls).forEach((key) => {
      const control = form.get(key);
      if (control?.errors?.['serverError']) {
        const { serverError, ...rest } = control.errors;
        control.setErrors(Object.keys(rest).length > 0 ? rest : null);
      }

      if (control instanceof FormGroup) {
        this.clearServerErrorOnChange(control);
      }
    });
  }
}
