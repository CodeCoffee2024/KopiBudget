import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { finalize } from 'rxjs';
import { AccountService } from '../../../core/services/account.service';
import { CategoryService } from '../../../core/services/category.service';
import { FormErrorService } from '../../../core/services/form-error.service';
import { LoadingService } from '../../../core/services/loading.service';
import { ToastService } from '../../../core/services/toast.service';
import { Account } from '../../../domain/models/account';
import { CategoryFragment } from '../../../domain/models/category';
import { InputTypes } from '../../../domain/models/input-type';
import { GenericDropdownListingOption } from '../../../domain/models/listing-option';
import { InputComponent } from '../../shared/components/input/input.component';
import { SoloSelectComponent } from '../../shared/components/solo-select/solo-select.component';

@Component({
	selector: 'app-account-create',
	standalone: true,
	imports: [CommonModule, ReactiveFormsModule, InputComponent, SoloSelectComponent],
	templateUrl: './account-create.component.html',
	styleUrls: ['./account-create.component.scss']
})
export class AccountCreateComponent {
	Account: Account = new Account();
	InputTypes = InputTypes;
	isDropdownLoading = false;
	categories: CategoryFragment[];
	hasMore = false;
	dropdownListingOption: GenericDropdownListingOption =
		new GenericDropdownListingOption();
	constructor(
		private categoryService: CategoryService,
		private loadingService: LoadingService,
		private toastService: ToastService,
		private formErrorService: FormErrorService,
		private accountService: AccountService,
		private activeModal: NgbActiveModal
	) {
	}
	onSubmit() {
		this.accountService.create(this.Account.toSubmit).pipe(
				finalize(() => {
					this.loadingService.hide();
				})
			)
			.subscribe({
				next: (result) => {
					this.activeModal.close(result);
				},
				error: (error) => {
					this.toastService.error(
						'Error',
						'Something went wrong.'
					);
					this.formErrorService.setServerErrors(this.Account.form, [
						error?.error?.error,
					]);
				},
			});
	}
	close() {
		this.activeModal.dismiss();
	}
	async onSearchChanged({
		search,
		page,
		clear = false,
	}: {
		search: string;
		page: number;
		clear: boolean;
	}) {
		this.isDropdownLoading = true;
		this.dropdownListingOption.search = search;
		this.dropdownListingOption.pageNumber = page;

		if (clear) {
			this.categories = [];
		}
		this.categoryService
			.dropdown(this.dropdownListingOption)
			.subscribe((it) => {
				this.hasMore =
					page * it.totalCount <
					it.totalPages * it.totalCount;
				const list = clear
					? it.data
					: [...this.categories, ...it.data];
				this.categories = list;
				this.isDropdownLoading = false;
			});
	}

	onSelectionChange(selected): void {
		this.Account.form
			.get('categoryId')
			.setValue(selected.item?.id);
		// this.refresh();
	}
	onAddSearchResult(categoryName) {
		this.loadingService.show();
		this.categoryService
			.create({name: categoryName})
			.pipe(
				finalize(() => {
					this.loadingService.hide();
				})
			)
			.subscribe({
				next: (result) => {
					this.Account.form.get("category").setValue(result.data);
					console.log(result);
					this.Account.form.get("categoryId").setValue(result.data.id);

					this.categoryService
						.dropdown(this.dropdownListingOption)
						.subscribe((it) => {
							this.categories = [...this.categories, ...it.data];
							this.isDropdownLoading = false;
						});
				},
				error: (error) => {
					this.toastService.error(
						'Error',
						'Something went wrong.'
					);
					this.formErrorService.setServerErrors(this.Account.form, [
						error?.error?.error,
					]);
				},
			});
	}
}
