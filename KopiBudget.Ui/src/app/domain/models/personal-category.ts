import { inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuditDto } from './audit';
import { GenericDropdownListingOption } from './listing-option';

export interface PersonalCategoryFragment {
	id: string;
	name: string;
	icon: string;
	color: string;
}
export interface PersonalCategoryDto extends AuditDto {
	name: string;
	icon: string;
	color: string;
}
export interface PersonalCategoryFieldDto {
	colors: KeyValueDto[];
	icons: KeyValueDto[];
}
export interface KeyValueDto {
	key: string;
	value: string;
}
export const PersonalCategoryConstants = {
	DELETECONFIRMATION: 'Are you sure you want to delete this personal category?',
	DELETESUCCESS: 'Personal category deleted successfully',
	UPDATESUCCESS: 'Personal category updated successfully',
	CREATESUCCESS: 'Personal category created successfully',
};
export class PersonalCategoryDropdownListingOption extends GenericDropdownListingOption {
	budgetId: string;
	budgetIds: string;
}

export class PersonalCategory {
	private fb = inject(FormBuilder);
	form: FormGroup = this.fb.group({
		icon: [null, [Validators.required]],
		color: [null, [Validators.required]],
		name: [null, [Validators.required]],
	});
	fill(personalCategory: PersonalCategoryDto) {
		this.form.patchValue({
			icon: personalCategory.icon,
			color: personalCategory.color,
			name: personalCategory.name,
		});
	}
	get toSubmit() {
		return {
			icon: this.form.get('icon').value.value,
			color: this.form.get('color').value.key,
			name: this.form.get('name').value,
		};
	}
	get serverErrors(): string[] {
		if (!this.form.errors) return [];
		return Object.values(this.form.errors) // array of objects
			.map((errObj) => Object.values(errObj)) // get inner arrays
			.flat(2) as string[]; // flatten to pure string array
	}
	selectIcon(icon) {
		this.form.get('icon').setValue(icon);
	}
	isSelectedIcon(icon) {
		return this.form.get('icon').value == icon;
	}
	selectColor(color) {
		this.form.get('color').setValue(color);
	}
	isSelectedColor(color) {
		return this.form.get('color').value == color;
	}
}
