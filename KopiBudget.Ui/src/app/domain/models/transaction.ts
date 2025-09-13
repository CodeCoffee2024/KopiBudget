import { inject } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AccountFragment } from "./account";
import { AuditDto } from "./audit";
import { CategoryFragment } from "./category";
import { GenericListingOption } from "./listing-option";

export class TransactionDto extends AuditDto {
    account: AccountFragment;
    amount: number;
    note: string;
    date: Date;
    category: CategoryFragment;
}
export const TransactionConstants = {
	DELETECONFIRMATION:
		'Are you sure you want to delete this transaction?',
	DELETESUCCESS: 'Transaction deleted successfully',
	UPDATESUCCESS: 'Transaction updated successfully',
	CREATESUCCESS: 'Transaction created successfully',
};
export class TransactionListingOption extends GenericListingOption {
    accountIds: [];
    categoryIds: [];
    dateFrom: string;
    dateTo: string;
}

export class Transaction {
    private fb = inject(FormBuilder);
    form: FormGroup = this.fb.group({
        account: [null, [Validators.required]],
        accountId: [null, [Validators.required]],
        category: [null, [Validators.required]],
        amount: [null, [Validators.required]],
        categoryId: [null, [Validators.required]],
        inputTime: [false],
        note: [""],
        time: [null],
        date: [null,  [Validators.required]],
    }, {
        validators: [this.timeRequiredIfInputTimeValidator]
    });

    searchForm: FormGroup = this.fb.group({
        search: [''],
        dateFrom: [null],
        accountIds: [null],
        categoryIds: [null],
        dateTo: [null],
    });

    get toSubmitSearchForm() {
        return {
            search: this.searchForm.get('search').value,
            dateFrom: this.searchForm.get('dateFrom').value,
            accountIds: this.searchForm.get('accountIds').value,
            categoryIds: this.searchForm.get('categoryIds').value,
            dateTo: this.searchForm.get('dateTo').value,
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
    get toSubmit() {
        return {
            accountId: this.form.get('accountId').value,
            categoryId: this.form.get('categoryId').value,
            amount: this.form.get('amount').value.toString(),
            inputTime: this.form.get('inputTime').value,
            note: this.form.get('note').value,
            time: this.form.get('time').value,
            date: this.form.get('date').value,
        }
    }
    fill(transaction: TransactionDto) {
        this.form.patchValue({
            account: transaction.account,
            accountId: transaction.account.id,
            category: transaction.category,
            categoryId: transaction.category.id,
            amount: transaction.amount,
            note: transaction.note,
        });
        const date = new Date(transaction.date);

        this.form.get("date")?.patchValue(date.toISOString().split("T")[0]);

        const hours = date.getHours().toString().padStart(2, '0');
        const minutes = date.getMinutes().toString().padStart(2, '0');
        this.form.get("time")?.patchValue(`${hours}:${minutes}`);
        this.form.get("inputTime")?.patchValue(date.getHours() > 0 || date.getMinutes() > 0);
    }

}
