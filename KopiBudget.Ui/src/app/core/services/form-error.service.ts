import { Injectable } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { toCamelCase } from '../../ui/shared/utils/string.util';

@Injectable({
  providedIn: 'root'
})
export class FormErrorService {
	injectServerErrorControl(form: FormGroup): void {
		if (!form.contains('serverError')) {
			form.addControl('serverError', new FormControl(''));
		}
	}

	setServerErrors(
		formGroup: FormGroup,
		errors: { name: string; description: string }[]
	): void {
		if (!errors) {
			return;
		}
		this.markFormGroupDirty(formGroup);
		this.clearServerErrorOnChange(formGroup);
		const globalErrors: Record<string, string[]> = {};

		errors.forEach(({ name, description }) => {
			const controlKey = toCamelCase(name);

			if (formGroup.controls[controlKey]) {
				const currentErrors =
					formGroup.controls[controlKey].errors || {};
				const serverErrors =
					currentErrors['serverError'] || [];

				formGroup.controls[controlKey].setErrors({
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
	markFormGroupDirty(formGroup: FormGroup): void {
		Object.keys(formGroup.controls).forEach((field) => {
			const control = formGroup.get(field);
			control?.markAsDirty({ onlySelf: true });

			if (control instanceof FormGroup) {
				this.markFormGroupDirty(control);
			}
		});
	}
	clearServerErrorOnChange(form: FormGroup): void {
		if (form.errors && form.errors['serverError']) {
			const { serverError, ...rest } = form.errors;
			form.setErrors(
				Object.keys(rest).length > 0 ? rest : null
			);
		}

		Object.keys(form.controls).forEach((key) => {
			const control = form.get(key);
			if (
				control &&
				control.errors &&
				control.errors['serverError']
			) {
				const { serverError, ...rest } = control.errors;
				control.setErrors(
					Object.keys(rest).length > 0 ? rest : null
				);
			}

			if (control instanceof FormGroup) {
				this.clearServerErrorOnChange(control);
			}
		});
	}
}
