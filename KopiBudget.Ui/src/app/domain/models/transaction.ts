import { inject } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AccountFragment } from "./account";
import { AuditDto } from "./audit";
import { CategoryFragment } from "./category";
import { GenericListingOption } from "./listing-option";

export class TransactionDto extends AuditDto {
    account: AccountFragment;
    amount: number;
    date: boolean;
    category: CategoryFragment;
}
export class TransactionListingOption extends GenericListingOption {}

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
            amount: this.form.get('amount').value,
            inputTime: this.form.get('inputTime').value,
            note: this.form.get('note').value,
            time: this.form.get('time').value,
            date: this.form.get('date').value,
        }
    }
}
