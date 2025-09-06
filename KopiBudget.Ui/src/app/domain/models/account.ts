import { inject } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AuditDto } from "./audit";
import { CategoryFragment } from "./category";

export class AccountDto extends AuditDto {
    name: string;
    balance: number;
    isExpense: boolean;
    category: CategoryFragment;
}

export class Account {
    private fb = inject(FormBuilder);
    form: FormGroup = this.fb.group({
        name: ['', [Validators.required]],
        category: ['', [Validators.required]],
        balance: ['', [Validators.required]],
        categoryId: ['', [Validators.required]],
        isExpense: [false],
    });
    get toSubmit() {
        return {
            name: this.form.get('name').value,
            categoryId: this.form.get('categoryId').value?.id,
            isExpense: this.form.get('isExpense').value,
            balance: this.form.get('balance').value,
        }
    }
}
