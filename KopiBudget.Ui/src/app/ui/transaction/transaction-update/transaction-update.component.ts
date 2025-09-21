import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { finalize } from 'rxjs';
import { AccountService } from '../../../core/services/account.service';
import { CategoryService } from '../../../core/services/category.service';
import { FormErrorService } from '../../../core/services/form-error.service';
import { LoadingService } from '../../../core/services/loading.service';
import { ToastService } from '../../../core/services/toast.service';
import { TransactionService } from '../../../core/services/transaction.service';
import { AccountFragment } from '../../../domain/models/account';
import { CategoryFragment } from '../../../domain/models/category';
import { InputTypes } from '../../../domain/models/input-type';
import { GenericDropdownListingOption } from '../../../domain/models/listing-option';
import { Transaction, TransactionDto } from '../../../domain/models/transaction';
import { FieldDisplayComponent } from '../../shared/components/field-display/field-display.component';
import { InputComponent } from '../../shared/components/input/input.component';
import { SoloSelectComponent } from '../../shared/components/solo-select/solo-select.component';

@Component({
	selector: 'app-transaction-update',
	standalone: true,
	imports: [
		CommonModule,
		ReactiveFormsModule,
		InputComponent,
		SoloSelectComponent,
		FieldDisplayComponent,
	],
	templateUrl: './transaction-update.component.html',
	styleUrls: ['./transaction-update.component.scss'],
})
export class TransactionUpdateComponent implements OnInit {
	transaction: TransactionDto = new TransactionDto();
	Transaction: Transaction = new Transaction();
	InputTypes = InputTypes;
	isDropdownLoading = false;
	categories: CategoryFragment[];
	accounts: AccountFragment[];
	hasMoreCategory = false;
	hasMoreAccount = false;
	dropdownListingOption: GenericDropdownListingOption = new GenericDropdownListingOption();
	constructor(
		private categoryService: CategoryService,
		private loadingService: LoadingService,
		private toastService: ToastService,
		private formErrorService: FormErrorService,
		private accountService: AccountService,
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
		this.Transaction.fill(this.transaction);
	}
	onSubmit() {
		this.transactionService
			.update(
				this.transaction.id,
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
		return this.Transaction.form.get('accountId').value;
	}
}
