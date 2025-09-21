import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { finalize } from 'rxjs';
import { BudgetPersonalCategoryService } from '../../../core/services/budget-personal-category.service';
import { FormErrorService } from '../../../core/services/form-error.service';
import { LoadingService } from '../../../core/services/loading.service';
import { ToastService } from '../../../core/services/toast.service';
import {
	budgetPersonalCategory,
	BudgetPersonalCategoryDto,
} from '../../../domain/models/budget-personal-category';
import { InputTypes } from '../../../domain/models/input-type';
import { PersonalCategoryDto } from '../../../domain/models/personal-category';
import { InputComponent } from '../../shared/components/input/input.component';

@Component({
	selector: 'app-budget-personal-category-limit-update',
	standalone: true,
	imports: [CommonModule, ReactiveFormsModule, InputComponent],
	templateUrl: './budget-personal-category-limit-update.component.html',
	styleUrls: ['./budget-personal-category-limit-update.component.scss'],
})
export class BudgetPersonalCategoryLimitUpdateComponent implements OnInit {
	BudgetPersonalCategory: budgetPersonalCategory = new budgetPersonalCategory();
	budgetPersonalCategory: BudgetPersonalCategoryDto;
	id: string;
	InputTypes = InputTypes;
	formNav = 1;
	personalCategories: PersonalCategoryDto[] = [];
	constructor(
		private loadingService: LoadingService,
		private toastService: ToastService,
		private formErrorService: FormErrorService,
		private budgetPersonalCategoryService: BudgetPersonalCategoryService,
		private activeModal: NgbActiveModal,
	) {}
	ngOnInit(): void {
		this.BudgetPersonalCategory.fill(this.budgetPersonalCategory);
	}
	getError(formControl, name) {
		return this.formErrorService.getFormControlErrors(formControl, name);
	}
	onSubmit() {
		if (this.BudgetPersonalCategory.form.valid)
			this.budgetPersonalCategoryService
				.update(
					this.id,
					this.budgetPersonalCategory.personalCategory.id,
					this.BudgetPersonalCategory.toSubmit,
				)
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
						this.formErrorService.setServerErrors(this.BudgetPersonalCategory.form, [
							error?.error?.errors,
						]);
					},
				});
	}
	close() {
		this.activeModal.close(null);
	}
}
