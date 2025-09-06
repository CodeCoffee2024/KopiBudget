import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { AbstractControl, FormBuilder, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { finalize } from 'rxjs';
import { RegisterRequest } from '../../../core/auth/auth.model';
import { FormErrorService } from '../../../core/services/form-error.service';
import { LoadingService } from '../../../core/services/loading.service';
import { TitleService } from '../../../core/services/title.service';
import { ToastService } from '../../../core/services/toast.service';
import { UserService } from '../../../core/services/user.service';
import { InputTypes } from '../../../domain/models/input-type';
import { InputComponent } from '../../shared/components/input/input.component';

@Component({
  selector: 'app-register',
  standalone: true,
    imports: [CommonModule, InputComponent, ReactiveFormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  InputTypes = InputTypes;
  private fb = inject(FormBuilder);
  private userService = inject(UserService);
  constructor(
    private formErrorService: FormErrorService,
    private titleService: TitleService,
    private loadingservice: LoadingService,
    private toastService: ToastService,
    private router: Router,
  ){
    this.formErrorService.injectServerErrorControl(
      this.form
    );
    this.formErrorService.clearServerErrorOnChange(
      this.form
    );
    this.titleService.setTitle('Login');
  }

  form = this.fb.group({
    userName: ['', [Validators.required]],
    email: [
      '',
      [Validators.required, Validators.email],
    ],
    password: ['', Validators.required],
    retypePassword: ['', Validators.required],
    firstName: ['', Validators.required],
    middleName: [''],
    lastName: ['', Validators.required],
  },
			{
				validators: this.mustMatchValidator(
					'password',
					'retypePassword'
				),
			});

  loading = false;
  errorMessage = '';
  onSubmit() {
    this.formErrorService.clearServerErrorOnChange(
      this.form
    );
    const payload: RegisterRequest = {
      username: this.form.value.userName!,
      password: this.form.value.password!,
      email: this.form.value.email!,
      firstName: this.form.value.firstName!,
      lastName: this.form.value.lastName!,
      middleName: this.form.value.middleName!,
    };
    this.loadingservice.show();
    this.userService
      .register(payload)
      .pipe(
        finalize(() => {
          this.loadingservice.hide();
        })
      )
      .subscribe({
        next: () => {
          this.toastService.success("Account Created");
          this.router.navigate(['../login'])
        },
        error: (error) => {
          this.toastService.error(
            'Error',
            'Something went wrong.'
          );
          this.form.markAllAsTouched();
          this.form.markAsDirty();
          this.formErrorService.setServerErrors(this.form, [
            error?.error?.errors,
          ]);
          console.log(this.form)
        },
      });
    }
	private mustMatchValidator(
		passwordKey: string,
		confirmPasswordKey: string
	) {
		return (
			formGroup: AbstractControl
		): ValidationErrors | null => {
			const password = formGroup.get(passwordKey);
			const confirmPassword = formGroup.get(
				confirmPasswordKey
			);

			if (!password || !confirmPassword) return null;
			if (
				confirmPassword.errors &&
				!confirmPassword.errors[passwordKey]
			)
				return null;

			if (password.value !== confirmPassword.value) {
				confirmPassword.setErrors({
					mustMatch: passwordKey,
				});
			} else {
				confirmPassword.setErrors(null);
			}

			return null;
		};
	}
}
