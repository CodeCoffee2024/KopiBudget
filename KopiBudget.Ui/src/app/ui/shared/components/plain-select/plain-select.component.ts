import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';


@Component({
  selector: 'app-plain-select',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './plain-select.component.html',
  styleUrls: ['./plain-select.component.scss']
})
export class PlainSelectComponent {
  @Input() label: string = '';
  @Input() inputId = 'input';
  @Input() controlName!: string;
  @Input() erorField: string = '';
  @Input() formGroup!: FormGroup;
  @Input() field: string = '';
  @Input() options = [];
  @Output() onChangeEvent: EventEmitter<string> = new EventEmitter<string>();
  get formControl(): FormControl {
    return this.formGroup?.get(this.controlName) as FormControl;
  }
  get errors(): string[] {
    if (!this.formControl || !this.formControl.errors) return [];

    const errors = this.formControl.errors;
    const errorMessages: string[] = [];
    const seen = new Set<string>();

    for (const errorKey of Object.keys(errors)) {
      const value = errors[errorKey];

      let message: string | null = null;

      if (errorKey === 'required') {
        message = `${this.label} is required`;
      } else if (errorKey === 'serverError') {
        if (Array.isArray(value)) {
        value.forEach((msg: string) => {
          if (!seen.has(msg)) {
          errorMessages.push(msg);
          seen.add(msg);
          }
        });
        } else {
        message = value;
        }
      } else {
        message = `${this.label} ${value}`;
      }

      if (message && !seen.has(message)) {
        errorMessages.push(message);
        seen.add(message);
      }
    }

    return errorMessages;
  }

  get isRequired(): boolean {
    return !!this.formControl?.errors?.['required'];
  }
  onChange(value) {
    this.onChangeEvent.emit(value.target.value)
  }
}

