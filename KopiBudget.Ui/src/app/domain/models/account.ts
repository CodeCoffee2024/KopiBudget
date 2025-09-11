import { inject } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AuditDto } from "./audit";
import { CategoryFragment } from "./category";

export class AccountDto extends AuditDto {
    name: string;
    balance: number;
    isDebt: boolean;
    category: CategoryFragment;
}
export class AccountFragment {
    id: string;
    name: string;
    balance: number;
    isDebt: boolean;
}

export class Account {
    private fb = inject(FormBuilder);
    form: FormGroup = this.fb.group({
        name: ['', [Validators.required]],
        category: ['', [Validators.required]],
        balance: ['', [Validators.required]],
        categoryId: ['', [Validators.required]],
        IsDebt: [false],
    });
    get toSubmit() {
        return {
            name: this.form.get('name').value,
            categoryId: this.form.get('categoryId').value,
            IsDebt: this.form.get('IsDebt').value,
            balance: this.form.get('balance').value,
        }
    }
}
