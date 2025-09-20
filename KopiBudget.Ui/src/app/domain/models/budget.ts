import { inject } from "@angular/core";
import { FormArray, FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AuditDto } from "./audit";
import { GenericListingOption } from "./listing-option";
import { PersonalCategoryDto, PersonalCategoryFragment } from "./personal-category";

export class BudgetDto extends AuditDto {
    amount: number;
    name: string;
    startDate: Date;
    endDate: Date;
    spentBudget: number;
    remainingBudget: number;
    spentBudgetPercentage: string;
    remainingBudgetPercentage: string;
    budgetPersonalCategories: BudgetPersonalCategory[] = [];
}
export class BudgetFragment {
	name: string;
    amount: number;
	id: string;
}

export interface BudgetPersonalCategory {
    personalCategory: PersonalCategoryFragment;
    spentBudget: number;
    remainingBudget: number;
    spentBudgetPercentage: string;
    remainingBudgetPercentage: string;
    limit: number;
}
export const BudgetConstants = {
    DELETECONFIRMATION:
        'Are you sure you want to delete this budget?',
    DELETESUCCESS: 'Budget deleted successfully',
    UPDATESUCCESS: 'Budget updated successfully',
    CREATESUCCESS: 'Budget created successfully',
};
export class BudgetListingOption extends GenericListingOption {
    categoryIds: [];
    startDate: string;
    endDate: string;
}

export class Budget {
    private fb = inject(FormBuilder);
    summaryForm: FormGroup = this.fb.group({
        startDate: [null, [Validators.required]],
        endDate: [null, [Validators.required]],
        name: [null, [Validators.required]],
        amount: [null, [Validators.required]],
    },  {
        validators: [this.dateValid]
    });
    globalErrors : string[] = [];
    dateValid(formGroup: FormGroup) {
        const startDateControl = formGroup.get('startDate');
        const endDateControl = formGroup.get('endDate');

        const startDate = startDateControl?.value;
        const endDate = endDateControl?.value;

        if (startDate && endDate && startDate > endDate) {
            startDateControl?.setErrors({
                ...(startDateControl.errors || {}),
                date: "must be less than or equal to end date",
            });
            endDateControl?.setErrors({
                ...(endDateControl.errors || {}),
                date: "must be greater than or equal to start date",
            });
            return { date: true };
        } else {
            if (startDateControl?.errors) {
                const { date, ...other } = startDateControl.errors;
                startDateControl.setErrors(Object.keys(other).length ? other : null);
            }
            if (endDateControl?.errors) {
                const { date, ...other } = endDateControl.errors;
                endDateControl.setErrors(Object.keys(other).length ? other : null);
            }
        }

        return null;
    }

    budgetPersonalCategoryForm: FormGroup = this.fb.group({
        budgetPersonalCategories: this.fb.array([], [Validators.required]),
    });
    markFormGroupDirty(formGroup: FormGroup) {
        Object.values(formGroup.controls).forEach(control => {
            if (control instanceof FormGroup) {
            this.markFormGroupDirty(control);
            } else if (control instanceof FormArray) {
                control.controls.forEach(c => {
                    if (c instanceof FormGroup) {
                        this.markFormGroupDirty(c);
                    } else {
                        c.markAsDirty();
                        c.markAsTouched();
                    }
                });
            } else {
                control.markAsDirty();
                control.markAsTouched();
            }
        });
    }
    detailedForm: FormGroup = this.fb.group({
    });
    get budgetPersonalCategories(): FormArray {
        return this.budgetPersonalCategoryForm.get('budgetPersonalCategories') as FormArray;
    }
    get toSubmit() {
        return {
            startDate: this.summaryForm.get('startDate').value,
            endDate: this.summaryForm.get('endDate').value,
            name: this.summaryForm.get('name').value,
            amount: this.summaryForm.get('amount').value.toString(),
            budgetPersonalCategories: this.budgetPersonalCategories.value.map((cat: any) => ({
                personalCategoryId: cat.id,
                limit: cat.limit.toString(),
            })),
        }
    }
    fill(budget: BudgetDto) {
        this.summaryForm.patchValue({
            startDate: budget.startDate,
            endDate: budget.endDate,
            name: budget.name,
            amount: budget.amount,
        });
        this.detailedForm.patchValue({
            budgetPersonalCategories: budget.budgetPersonalCategories,
        });
    }
    toggleCategory(cat: PersonalCategoryDto) {
        const index = this.budgetPersonalCategories.controls.findIndex(
            (x) => x.value.id === cat.id
        );

        if (index >= 0) {
            this.budgetPersonalCategories.removeAt(index);
        } else {
            this.budgetPersonalCategories.push(
            this.fb.group({
                id: [cat.id],
                name: [cat.name],
                icon: [cat.icon],
                color: [cat.color],
                limit: [null],
            })
            );
        }
    }

    addLimitRequired() {
        this.budgetPersonalCategories.controls.forEach((ctrl) => {
            const group = ctrl as FormGroup;
            group.get('limit')?.setValidators([Validators.required]);
            group.get('limit')?.updateValueAndValidity();
        });
    }
    removeLimitRequired() {
        this.budgetPersonalCategoryForm = this.fb.group({
            budgetPersonalCategories: this.fb.array([], [Validators.required]),
        });
    }
    isSelected(cat: PersonalCategoryDto): boolean {
        return this.budgetPersonalCategories.value.some((c: PersonalCategoryDto) => c.name === cat.name);
    }
    get remainingAllocation() {
        let total = 0;
        this.budgetPersonalCategories.controls.forEach((ctrl) => {
            const group = ctrl as FormGroup;
            total += group.get('limit')?.value;
        });
        return this.summaryForm.get('amount').value - total;
    }
    get serverErrors(): string[] {
        if (!this.summaryForm.errors) return [];
        return Object.values(this.summaryForm.errors) // array of objects
            .map(errObj => Object.values(errObj))       // get inner arrays
            .flat(2) as string[];                       // flatten to pure string array
        }
    }
