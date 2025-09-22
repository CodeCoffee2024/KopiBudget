import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { finalize } from 'rxjs';
import { BudgetService } from '../../../core/services/budget.service';
import { FormErrorService } from '../../../core/services/form-error.service';
import { LoadingService } from '../../../core/services/loading.service';
import { ToastService } from '../../../core/services/toast.service';
import { Budget, BudgetDto } from '../../../domain/models/budget';
import { InputTypes } from '../../../domain/models/input-type';
import { PersonalCategoryDto } from '../../../domain/models/personal-category';
import { InputComponent } from '../../shared/components/input/input.component';

@Component({
	selector: 'app-budget-update',
	standalone: true,
	imports: [CommonModule, ReactiveFormsModule, InputComponent],
	templateUrl: './budget-update.component.html',
	styleUrls: ['./budget-update.component.scss'],
})
export class BudgetUpdateComponent implements OnInit {
	Budget: Budget = new Budget();
	budget: BudgetDto = new BudgetDto();
	InputTypes = InputTypes;
	formNav = 1;
	personalCategories: PersonalCategoryDto[] = [];
	constructor(
		private loadingService: LoadingService,
		private toastService: ToastService,
		private formErrorService: FormErrorService,
		private budgetService: BudgetService,
		private activeModal: NgbActiveModal,
	) {}
	ngOnInit(): void {
		this.Budget.fill(this.budget);
	}
	getError(formControl, name) {
		return this.formErrorService.getFormControlErrors(formControl, name);
	}
	onSubmitSummary() {
		if (!this.Budget.summaryForm.valid) {
			this.Budget.summaryForm.markAllAsTouched();
			this.Budget.markFormGroupDirty(this.Budget.summaryForm);
			return;
		}
		this.formNav = 2;
	}
	back() {
		this.Budget.removeLimitRequired();
		this.formNav--;
	}
	onSubmitPersonalCategories() {
		this.Budget.globalErrors = this.formErrorService.getFormControlErrors(
			this.Budget.budgetPersonalCategoryForm.get('budgetPersonalCategories'),
			'Category',
		);
		if (!this.Budget.budgetPersonalCategoryForm.valid) {
			this.Budget.budgetPersonalCategoryForm.markAllAsTouched();
			this.Budget.budgetPersonalCategoryForm.markAsDirty();
			return;
		}
		this.Budget.addLimitRequired();
		this.formNav = 3;
	}
	onSubmit() {
		if (this.Budget.budgetPersonalCategoryForm.valid && this.Budget.summaryForm.valid)
			this.budgetService
				.update(this.budget.id, this.Budget.toSubmit)
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
						this.formErrorService.setServerErrors(this.Budget.summaryForm, [
							error?.error?.errors,
						]);
					},
				});
	}
	close() {
		this.activeModal.close(null);
	}
}
