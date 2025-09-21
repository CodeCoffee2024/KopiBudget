import { inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountFragment } from './account';
import { AuditDto } from './audit';
import { BudgetFragment } from './budget';
import { CategoryFragment } from './category';
import { GenericListingOption } from './listing-option';
import { PersonalCategoryFragment } from './personal-category';

export class TransactionDto extends AuditDto {
  account: AccountFragment;
  amount: number;
  budget: BudgetFragment;
  personalCategory: PersonalCategoryFragment;
  note: string;
  date: Date;
  category: CategoryFragment;
}
export const TransactionConstants = {
  DELETECONFIRMATION: 'Are you sure you want to delete this transaction?',
  DELETESUCCESS: 'Transaction deleted successfully',
  UPDATESUCCESS: 'Transaction updated successfully',
  CREATESUCCESS: 'Transaction created successfully',
};
export const TransactionTypes = {
  ACCOUNT: 'accou',
  BUDGET: 'budge',
};
export class TransactionListingOption extends GenericListingOption {
  accountIds: [];
  categoryIds: [];
  budgetIds: [];
  personalCategoryIds: [];
  dateFrom: string;
  dateTo: string;
  type: string = TransactionTypes.ACCOUNT;
}
export enum TransactionDropdowns {
  ACCOUNT,
  CATEGORY,
  PERSONALCATEGORY,
  BUDGET,
}

export class Transaction {
  private fb = inject(FormBuilder);
  form: FormGroup = this.fb.group(
    {
      account: [null],
      accountId: [null],
      category: [null],
      categoryId: [null],
      personalCategory: [null],
      personalCategoryId: [null],
      amount: [null, [Validators.required]],
      budget: [null],
      budgetId: [null],
      inputTime: [false],
      type: ['', Validators.required],
      note: [''],
      time: [null],
      date: [null, [Validators.required]],
    },
    {
      validators: [this.timeRequiredIfInputTimeValidator, this.typeValidator],
    },
  );
  searchForm: FormGroup = this.fb.group({
    search: [''],
    dateFrom: [null],
    accountIds: [null],
    categoryIds: [null],
    budgetIds: [null],
    personalCategoryIds: [null],
    dateTo: [null],
  });

  get toSubmitSearchForm() {
    return {
      search: this.searchForm.get('search').value,
      dateFrom: this.searchForm.get('dateFrom').value,
      accountIds: this.searchForm.get('accountIds').value,
      budgetIds: this.searchForm.get('budgetIds').value,
      personalCategoryIds: this.searchForm.get('personalCategoryIds').value,
      categoryIds: this.searchForm.get('categoryIds').value,
      dateTo: this.searchForm.get('dateTo').value,
    };
  }
  typeValidator(formGroup: FormGroup) {
    const type = formGroup.get('type')?.value;
    if (type === TransactionTypes.ACCOUNT) {
      formGroup
        .get('category')
        ?.setErrors(!formGroup.get('category')?.value ? { required: true } : null);
      formGroup
        .get('categoryId')
        ?.setErrors(!formGroup.get('categoryId')?.value ? { required: true } : null);
      formGroup
        .get('account')
        ?.setErrors(!formGroup.get('account')?.value ? { required: true } : null);
      formGroup
        .get('accountId')
        ?.setErrors(!formGroup.get('accountId')?.value ? { required: true } : null);
    } else if (type === TransactionTypes.BUDGET) {
      formGroup
        .get('budget')
        ?.setErrors(!formGroup.get('budget')?.value ? { required: true } : null);
      formGroup
        .get('budgetId')
        ?.setErrors(!formGroup.get('budgetId')?.value ? { required: true } : null);
      formGroup
        .get('personalCategory')
        ?.setErrors(!formGroup.get('personalCategory')?.value ? { required: true } : null);
      formGroup
        .get('personalCategoryId')
        ?.setErrors(!formGroup.get('personalCategoryId')?.value ? { required: true } : null);
      formGroup
        .get('budgetId')
        ?.setErrors(!formGroup.get('budgetId')?.value ? { required: true } : null);
    }
  }
  timeRequiredIfInputTimeValidator(formGroup: FormGroup) {
    const inputTime = formGroup.get('inputTime')?.value;
    const time = formGroup.get('time')?.value;

    if (inputTime && !time) {
      formGroup.get('time')?.setErrors({ required: true });
    } else {
      if (formGroup.get('time')?.hasError('required')) {
        formGroup.get('time')?.setErrors(null);
      }
    }

    return null;
  }
  get toSubmitTransactionForm() {
    return {
      accountId: this.form.get('accountId').value,
      categoryId: this.form.get('categoryId').value,
      amount: this.form.get('amount').value?.toString(),
      inputTime: this.form.get('inputTime').value,
      note: this.form.get('note').value,
      type: this.form.get('type').value,
      time: this.form.get('time').value,
      date: this.form.get('date').value,
    };
  }
  get toSubmitBudgetForm() {
    return {
      budgetId: this.form.get('budgetId').value,
      personalCategoryId: this.form.get('personalCategoryId').value,
      amount: this.form.get('amount').value?.toString(),
      inputTime: this.form.get('inputTime').value,
      note: this.form.get('note').value,
      type: this.form.get('type').value,
      time: this.form.get('time').value,
      date: this.form.get('date').value,
    };
  }
  fill(transaction: TransactionDto) {
    this.form.patchValue({
      account: transaction.account,
      accountId: transaction.account?.id,
      category: transaction.category,
      categoryId: transaction.category?.id,
      budgetId: transaction.budget?.id,
      personalCategoryId: transaction.personalCategory?.id,
      budget: transaction.budget,
      type: transaction.account?.id ? TransactionTypes.ACCOUNT : TransactionTypes.BUDGET,
      personalCategory: transaction.personalCategory,
      amount: transaction.amount,
      note: transaction.note,
    });
    const date = new Date(transaction.date);

    this.form.get('date')?.patchValue(date.toISOString().split('T')[0]);

    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    this.form.get('time')?.patchValue(`${hours}:${minutes}`);
    this.form.get('inputTime')?.patchValue(date.getHours() > 0 || date.getMinutes() > 0);
  }
}
