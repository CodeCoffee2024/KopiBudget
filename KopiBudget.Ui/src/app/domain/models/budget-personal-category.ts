import { inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BudgetFragment } from './budget';
import { PersonalCategoryFragment } from './personal-category';

export interface BudgetPersonalCategoryFragment {
	personalCategory: PersonalCategoryFragment;
	spentBudget: number;
	remainingBudget: number;
	spentBudgetPercentage: string;
	remainingBudgetPercentage: string;
	limit: number;
}
export const BudgetPersonalCategoryConstants = {
	DELETECONFIRMATION: 'Are you sure you want to delete this personal category?',
	DELETESUCCESS: 'Personal category deleted successfully',
	UPDATESUCCESS: 'Personal category updated successfully',
	CREATESUCCESS: 'Personal category created successfully',
};
export interface BudgetPersonalCategoryDto {
	budget: BudgetFragment;
	personalCategory: PersonalCategoryFragment;
	spentBudget: number;
	remainingBudget: number;
	remainingLimit: number;
	spentBudgetPercentage: string;
	remainingBudgetPercentage: string;
	limit: number;
}
export class budgetPersonalCategory {
	private fb = inject(FormBuilder);
	form: FormGroup = this.fb.group({
		limit: [null, [Validators.required]],
		remainingBudget: [null],
	});
	fill(budgetPersonalCategory: BudgetPersonalCategoryDto) {
		this.form.patchValue({
			limit: budgetPersonalCategory.limit,
			remainingBudget: Number(budgetPersonalCategory.remainingLimit),
		});
	}
	get toSubmit() {
		return { limit: this.form.get('limit').value.toString() };
	}
	get serverErrors(): string[] {
		if (!this.form.errors) return [];
		return Object.values(this.form.errors) // array of objects
			.map((errObj) => Object.values(errObj)) // get inner arrays
			.flat(2) as string[]; // flatten to pure string array
	}
	get remainingBudget() {
		const remaining = Number(this.form.get('remainingBudget')?.value ?? 0);
		const limit = Number(this.form.get('limit')?.value ?? 0);

		return Math.max(0, remaining - limit);
	}
}
