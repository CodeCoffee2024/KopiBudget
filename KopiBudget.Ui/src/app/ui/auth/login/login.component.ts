import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { finalize } from 'rxjs';
import { LoginRequest } from '../../../core/auth/auth.model';
import { AuthService } from '../../../core/auth/auth.service';
import { FormErrorService } from '../../../core/services/form-error.service';
import { LoadingService } from '../../../core/services/loading.service';
import { TitleService } from '../../../core/services/title.service';
import { ToastService } from '../../../core/services/toast.service';
import { InputTypes } from '../../../domain/models/input-type';
import { InputComponent } from '../../shared/components/input/input.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, InputComponent, ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  InputTypes = InputTypes;
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  constructor(
    private formErrorService: FormErrorService,
    private titleService: TitleService,
    private loadingservice: LoadingService,
    private toastService: ToastService,
    private router: Router,
  ) {
    this.formErrorService.injectServerErrorControl(this.form);
    this.formErrorService.clearServerErrorOnChange(this.form);
    this.titleService.setTitle('Login');
  }

  form = this.fb.group({
    usernameEmail: ['', [Validators.required]],
    password: ['', [Validators.required]],
  });

  loading = false;
  errorMessage = '';
  onSubmit() {
    this.formErrorService.clearServerErrorOnChange(this.form);
    const credentials: LoginRequest = {
      usernameEmail: this.form.value.usernameEmail!,
      password: this.form.value.password!,
    };
    this.loadingservice.show();
    this.authService
      .login(credentials)
      .pipe(
        finalize(() => {
          this.loadingservice.hide();
        }),
      )
      .subscribe({
        next: () => {
          this.router.navigate(['admin']);
        },
        error: (error) => {
          this.toastService.error('Error', 'Something went wrong.');
          this.form.markAllAsTouched();
          this.form.markAsDirty();
          this.formErrorService.setServerErrors(this.form, [error?.error?.error]);
        },
      });
  }
}
