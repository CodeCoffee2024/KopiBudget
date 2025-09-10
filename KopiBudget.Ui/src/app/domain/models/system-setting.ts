import { inject } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AuditDto } from "./audit";

export class SystemSettingDto extends AuditDto {
    currency: string;
}

export class SystemSetting {
    private fb = inject(FormBuilder);
    formCurrency: FormGroup = this.fb.group({
        currency: ['', [Validators.required]]
    });
    get toSubmitFormCurrency() {
        return {
            currency: this.formCurrency.get('currency').value?.code,
        }
    }
}
