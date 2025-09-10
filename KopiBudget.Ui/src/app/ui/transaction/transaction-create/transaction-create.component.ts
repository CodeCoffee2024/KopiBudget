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
import { Transaction } from '../../../domain/models/transaction';
import { InputComponent } from '../../shared/components/input/input.component';
import { SoloSelectComponent } from '../../shared/components/solo-select/solo-select.component';

@Component({
  selector: 'app-transaction-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, InputComponent, SoloSelectComponent],
  templateUrl: './transaction-create.component.html',
  styleUrls: ['./transaction-create.component.scss']
})
export class TransactionCreateComponent implements OnInit {
  Transaction: Transaction = new Transaction();
  InputTypes = InputTypes;
  isDropdownLoading = false;
  categories: CategoryFragment[];
  accounts: AccountFragment[];
  hasMoreCategory = false;
  hasMoreAccount = false;
  dropdownListingOption: GenericDropdownListingOption =
    new GenericDropdownListingOption();
  constructor(
    private categoryService: CategoryService,
    private loadingService: LoadingService,
    private toastService: ToastService,
    private formErrorService: FormErrorService,
    private accountService: AccountService,
    private transactionService: TransactionService,
    private activeModal: NgbActiveModal
  ) {

  }
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
    this.transactionService.create(this.Transaction.toSubmit).pipe(
        finalize(() => {
          this.loadingService.hide();
        })
      )
      .subscribe({
        next: (result) => {
          this.activeModal.close(result);
        },
        error: (error) => {
          this.Transaction.form.markAllAsTouched();
          this.Transaction.form.markAsDirty();
          this.toastService.error(
            'Error',
            'Something went wrong.'
          );
          this.formErrorService.setServerErrors(this.Transaction.form, [
            error?.error?.error,
          ]);
        },
      });
  }
  close() {
    this.activeModal.dismiss();
  }
  async onSearchChangedAccount({
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
      this.accounts = [];
    }
    this.accountService
      .dropdown(this.dropdownListingOption)
      .subscribe((it) => {
        this.hasMoreAccount =
          page * it.totalCount <
          it.totalPages * it.totalCount;
        const list = clear
          ? it.data
          : [...this.accounts, ...it.data];
        this.accounts = list;
        this.isDropdownLoading = false;
      });
  }

  onSelectionChangeAccount(selected): void {
    this.Transaction.form
      .get('accountId')
      .setValue(selected.item?.id);
    // this.refresh();
  }

  async onSearchChangedCategory({
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
        this.hasMoreAccount =
          page * it.totalCount <
          it.totalPages * it.totalCount;
        const list = clear
          ? it.data
          : [...this.categories, ...it.data];
        this.categories = list;
        this.isDropdownLoading = false;
      });
  }

  onSelectionChangeCategory(selected): void {
    this.Transaction.form
      .get('categoryId')
      .setValue(selected.item?.id);
    // this.refresh();
  }
  onAddSearchResultCategory(categoryName) {
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
          this.Transaction.form.get("category").setValue(result.data);
          this.Transaction.form.get("categoryId").setValue(result.data.id);

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
          this.formErrorService.setServerErrors(this.Transaction.form, [
            error?.error?.error,
          ]);
        },
      });
  }
}
