import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { finalize } from 'rxjs';
import { FormErrorService } from '../../../core/services/form-error.service';
import { LoadingService } from '../../../core/services/loading.service';
import { PersonalCategoryService } from '../../../core/services/personal-category.service';
import { ToastService } from '../../../core/services/toast.service';
import { InputTypes } from '../../../domain/models/input-type';
import {
	PersonalCategory,
	PersonalCategoryFieldDto,
} from '../../../domain/models/personal-category';
import { InputComponent } from '../../shared/components/input/input.component';

@Component({
	selector: 'app-personal-category-create',
	standalone: true,
	imports: [CommonModule, ReactiveFormsModule, InputComponent],
	templateUrl: './personal-category-create.component.html',
	styleUrls: ['./personal-category-create.component.scss'],
})
export class PersonalCategoryCreateComponent implements OnInit {
	PersonalCategory: PersonalCategory = new PersonalCategory();
	InputTypes = InputTypes;
	fields: PersonalCategoryFieldDto;
	constructor(
		private activeModal: NgbActiveModal,
		private personalCategoryService: PersonalCategoryService,
		private loadingService: LoadingService,
		private toastService: ToastService,
		private formErrorService: FormErrorService,
	) {}
	ngOnInit(): void {}
	onSubmit() {
		if (this.PersonalCategory.form.valid)
			this.personalCategoryService
				.create(this.PersonalCategory.toSubmit)
				.pipe(
					finalize(() => {
						this.loadingService.hide();
					}),
				)
				.subscribe({
					next: (result) => {
						this.activeModal.close(result);
					},
					error: (error) => {
						this.toastService.error('Error', 'Something went wrong.');
						this.formErrorService.setServerErrors(this.PersonalCategory.form, [
							error?.error?.errors,
						]);
					},
				});
	}
	close() {
		this.activeModal.close(null);
	}
}
