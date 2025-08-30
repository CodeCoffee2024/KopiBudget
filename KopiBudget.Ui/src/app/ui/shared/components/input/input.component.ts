import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ControlValueAccessor, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { InputTypes } from '../../../../domain/models/input-type';

@Component({
  selector: 'app-input',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss']
})
export class InputComponent implements ControlValueAccessor {
	@Input() inputType: InputTypes = InputTypes.Text;
	@Input() label: string = '';
	@Input() hideLabel = false;
	@Input() inputId = 'input';
	@Input() controlName!: string;
	@Input() erorField: string = '';
	@Input() formGroup!: FormGroup;

	isPasswordVisible = false;

	get formControl(): FormControl {
		return this.formGroup?.get(this.controlName) as FormControl;
	}

	togglePasswordVisibility(): void {
		this.isPasswordVisible = !this.isPasswordVisible;
	}

	get errors(): string[] {
		if (!this.formControl || !this.formControl.errors || !this.formControl.touched) return [];

		const errors = this.formControl.errors;
		const errorMessages: string[] = [];

		for (const errorKey of Object.keys(errors)) {
		const value = errors[errorKey];
		if (errorKey === 'required') {
			errorMessages.push(`${this.label} is required`);
		} else if (errorKey === 'email') {
			errorMessages.push(`${this.label} must be a valid email`);
		} else if (errorKey === 'mustMatch') {
			errorMessages.push(`${this.label} must match ${value}`);
		} else if (errorKey === 'minlength') {
			const min = value?.requiredLength;
			errorMessages.push(`${this.label} must be at least ${min} characters`);
		} else if (errorKey === 'serverError') {
			if (Array.isArray(value)) {
			value.forEach((msg: string) => errorMessages.push(msg));
			} else {
			errorMessages.push(value);
			}
		} else {
			errorMessages.push(
			`${this.label} ${errorKey.replace(/([A-Z])/g, ' $1').toLowerCase()}`
			);
		}
		}

		return errorMessages;
	}

	get isRequired(): boolean {
		return !!this.formControl?.errors?.['required'];
	}

	onInput(event: Event): void {
		const input = event.target as HTMLInputElement;

		if (this.inputType === InputTypes.Month) {
			const value = input.value;
			if (!/^\d{4}-\d{2}$/.test(value)) {
				this.formControl.setValue('', { emitEvent: false });
			}
		}

		if (this.inputType === 'number') {
			const regex = /^\d*(\.\d{0,2})?$/;
			if (!regex.test(input.value)) {
				input.value = input.value.slice(0, -1);
				this.formControl.setValue(input.value, { emitEvent: false });
			}
		}
	}

	onBlur(): void {
		this.formControl.markAsTouched(); // ✅ only mark THIS field touched
		console.log(this.formControl);
		if (this.inputType === 'number') {
			let value = parseFloat(this.formControl.value);
			if (!isNaN(value)) {
				value = parseFloat(value.toFixed(2));
				this.formControl.setValue(value, { emitEvent: false });
			}
		}
	}

	writeValue(value: any): void {
		if (this.formControl) {
		this.formControl.setValue(value, { emitEvent: false });
		}
	}

	registerOnChange(fn: any): void {
		this.formControl?.valueChanges.subscribe(fn);
	}

	registerOnTouched(fn: any): void {
		this.formControl?.registerOnChange(fn);
	}

	setDisabledState(isDisabled: boolean): void {
		if (isDisabled) {
			this.formControl.disable();
		} else {
			this.formControl.enable();
		}
	}
}
