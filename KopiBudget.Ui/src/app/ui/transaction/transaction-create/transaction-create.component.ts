import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { finalize, Observable } from 'rxjs';
import { AccountService } from '../../../core/services/account.service';
import { BudgetService } from '../../../core/services/budget.service';
import { CategoryService } from '../../../core/services/category.service';
import { FormErrorService } from '../../../core/services/form-error.service';
import { LoadingService } from '../../../core/services/loading.service';
import { PersonalCategoryService } from '../../../core/services/personal-category.service';
import { ToastService } from '../../../core/services/toast.service';
import { TransactionService } from '../../../core/services/transaction.service';
import { AccountFragment } from '../../../domain/models/account';
import { BudgetFragment } from '../../../domain/models/budget';
import { CategoryFragment } from '../../../domain/models/category';
import { InputTypes } from '../../../domain/models/input-type';
import { GenericDropdownListingOption } from '../../../domain/models/listing-option';
import {
	PersonalCategoryDropdownListingOption,
	PersonalCategoryFragment,
} from '../../../domain/models/personal-category';
import {
	Transaction,
	TransactionDropdowns,
	TransactionTypes,
} from '../../../domain/models/transaction';
import { InputComponent } from '../../shared/components/input/input.component';
import { PlainSelectComponent } from '../../shared/components/plain-select/plain-select.component';
import { SoloSelectComponent } from '../../shared/components/solo-select/solo-select.component';

@Component({
	selector: 'app-transaction-create',
	standalone: true,
	imports: [
		CommonModule,
		ReactiveFormsModule,
		InputComponent,
		SoloSelectComponent,
		PlainSelectComponent,
	],
	templateUrl: './transaction-create.component.html',
	styleUrls: ['./transaction-create.component.scss'],
})
export class TransactionCreateComponent implements OnInit {
	Transaction: Transaction = new Transaction();
	TransactionDropdowns = TransactionDropdowns;
	typeOptions = [
		{
			name: 'Account',
			value: TransactionTypes.ACCOUNT,
		},
		{
			name: 'Budget',
			value: TransactionTypes.BUDGET,
		},
	];
	InputTypes = InputTypes;
	isDropdownLoading = false;
	categories: CategoryFragment[];
	budgets: BudgetFragment[];
	personalCategories: PersonalCategoryFragment[];
	accounts: AccountFragment[];
	hasMoreCategory = false;
	hasMoreAccount = false;
	hasMoreBudget = false;
	hasMorePersonalCategory = false;
	dropdownListingOption: GenericDropdownListingOption = new GenericDropdownListingOption();
	personalCategoryDropdownListingOption: PersonalCategoryDropdownListingOption =
		new PersonalCategoryDropdownListingOption();
	constructor(
		private categoryService: CategoryService,
		private loadingService: LoadingService,
		private toastService: ToastService,
		private formErrorService: FormErrorService,
		private accountService: AccountService,
		private budgetService: BudgetService,
		private personalCategoryService: PersonalCategoryService,
		private transactionService: TransactionService,
		private activeModal: NgbActiveModal,
	) {}
	ngOnInit(): void {
		this.Transaction.form.get('inputTime')?.valueChanges.subscribe((inputTime) => {
			const timeControl = this.Transaction.form.get('time');
			if (inputTime) {
				timeControl?.setValidators([Validators.required]);
			} else {
				timeControl?.clearValidators();
			}
			timeControl?.updateValueAndValidity();
		});
	}
	onSubmit() {
		this.transactionService
			.create(
				this.isAccount
					? this.Transaction.toSubmitTransactionForm
					: this.Transaction.toSubmitBudgetForm,
			)
			.pipe(
				finalize(() => {
					this.loadingService.hide();
				}),
			)
			.subscribe({
				next: (result) => {
					this.activeModal.close(result);
				},
				error: (error) => {
					this.Transaction.form.markAllAsTouched();
					this.Transaction.form.markAsDirty();
					this.toastService.error('Error', 'Something went wrong.');
					this.formErrorService.setServerErrors(this.Transaction.form, [
						error?.error?.errors,
					]);
				},
			});
	}
	close() {
		this.activeModal.close(null);
	}
	get isAccount() {
		return this.Transaction.form.get('type').value == TransactionTypes.ACCOUNT;
	}
	get isBudget() {
		return this.Transaction.form.get('type').value == TransactionTypes.BUDGET;
	}
	onTypeChange(type) {
		this.Transaction.form.get('type').setValue(type);
	}
	async onSearchChanged(
		{ search, page, clear = false }: { search: string; page: number; clear: boolean },
		dropdownType: TransactionDropdowns,
	) {
		this.isDropdownLoading = true;
		this.dropdownListingOption.search = search;
		this.dropdownListingOption.pageNumber = page;
		this.loadDropdown(page, clear, dropdownType);
	}

	private loadDropdown(page: number, clear: boolean, dropdownType: TransactionDropdowns) {
		let service$: Observable<any>;
		let targetList: any[];

		switch (dropdownType) {
			case TransactionDropdowns.ACCOUNT:
				service$ = this.accountService.dropdown(this.dropdownListingOption);
				targetList = this.accounts;
				break;
			case TransactionDropdowns.CATEGORY:
				service$ = this.categoryService.dropdown(this.dropdownListingOption);
				targetList = this.categories;
				break;
			case TransactionDropdowns.BUDGET:
				service$ = this.budgetService.dropdown(this.dropdownListingOption);
				targetList = this.budgets;
				break;
			case TransactionDropdowns.PERSONALCATEGORY:
				this.personalCategoryDropdownListingOption.budgetId =
					this.Transaction.form.get('budgetId')?.value;
				this.personalCategoryDropdownListingOption.search =
					this.dropdownListingOption.search;
				this.personalCategoryDropdownListingOption.exclude;
				service$ = this.personalCategoryService.dropdown(
					this.personalCategoryDropdownListingOption,
				);
				targetList = this.personalCategories;
				break;
		}

		if (!service$) return;

		if (clear) {
			targetList = [];
		}

		service$.subscribe({
			next: (it) => {
				this.hasMoreAccount = page * it.totalCount < it.totalPages * it.totalCount;
				const list = clear ? it.data : [...targetList, ...it.data];

				switch (dropdownType) {
					case TransactionDropdowns.ACCOUNT:
						this.accounts = list;
						break;
					case TransactionDropdowns.CATEGORY:
						this.categories = list;
						break;
					case TransactionDropdowns.BUDGET:
						this.budgets = list;
						break;
					case TransactionDropdowns.PERSONALCATEGORY:
						this.personalCategories = list;
						break;
				}

				this.isDropdownLoading = false;
			},
			error: () => {
				this.isDropdownLoading = false;
			},
		});
	}

	onSelectionChange(selected: any, controlName: string): void {
		this.Transaction.form.get(controlName + 'Id')?.setValue(selected.item?.id);
	}

	onAddSearchResult(name: string, controlName: string, dropdownType: TransactionDropdowns) {
		this.loadingService.show();
		this.categoryService
			.create({ name })
			.pipe(finalize(() => this.loadingService.hide()))
			.subscribe({
				next: (result) => {
					this.Transaction.form.get(controlName)?.setValue(result.data);
					this.Transaction.form.get(controlName + 'Id')?.setValue(result.data.id);
					this.loadDropdown(this.dropdownListingOption.pageNumber, false, dropdownType);
				},
				error: (error) => {
					this.toastService.error('Error', 'Something went wrong.');
					this.formErrorService.setServerErrors(this.Transaction.form, [
						error?.error?.error,
					]);
				},
			});
	}
	get isBudgetNull() {
		return this.Transaction.form.get('budgetId').value;
	}
}
