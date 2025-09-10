import { CommonModule } from '@angular/common';
import { Component, EventEmitter } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { AccountService } from '../../../core/services/account.service';
import { CategoryService } from '../../../core/services/category.service';
import { FormErrorService } from '../../../core/services/form-error.service';
import { LoadingService } from '../../../core/services/loading.service';
import { ToastService } from '../../../core/services/toast.service';
import { AccountFragment } from '../../../domain/models/account';
import { CategoryFragment } from '../../../domain/models/category';
import { InputTypes } from '../../../domain/models/input-type';
import { GenericDropdownListingOption } from '../../../domain/models/listing-option';
import { Transaction } from '../../../domain/models/transaction';
import { InputComponent } from '../../shared/components/input/input.component';
import { SoloSelectComponent } from '../../shared/components/solo-select/solo-select.component';
import { MultiSelectComponent } from '../../shared/multi-select/multi-select.component';

@Component({
  selector: 'app-transaction-search',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, InputComponent, SoloSelectComponent, MultiSelectComponent],
  templateUrl: './transaction-search.component.html',
  styleUrls: ['./transaction-search.component.scss']
})
export class TransactionSearchComponent {
  Transaction: Transaction = new Transaction();
  InputTypes = InputTypes;
  isDropdownLoading = false;
  categories: CategoryFragment[];
  accounts: AccountFragment[];
  hasMoreCategory = false;
  hasMoreAccount = false;
  listingOptionAccount: GenericDropdownListingOption =
    new GenericDropdownListingOption();
  listingOptionCategory: GenericDropdownListingOption =
    new GenericDropdownListingOption();
  refreshEvent: EventEmitter<object> = new EventEmitter<object>();
  constructor(
    private categoryService: CategoryService,
    private loadingService: LoadingService,
    private toastService: ToastService,
    private formErrorService: FormErrorService,
    private accountService: AccountService,
  ) {

  }
  onSubmit() {
    // this.transactionService.create(this.Transaction.toSubmitSearchForm).pipe(
    //     finalize(() => {
    //       this.loadingService.hide();
    //     })
    //   )
    //   .subscribe({
    //     next: (result) => {
    //       this.activeModal.close(result);
    //     },
    //     error: (error) => {
    //       this.Transaction.searchForm.markAllAsTouched();
    //       this.Transaction.searchForm.markAsDirty();
    //       this.toastService.error(
    //         'Error',
    //         'Something went wrong.'
    //       );
    //       this.formErrorService.setServerErrors(this.Transaction.searchForm, [
    //         error?.error?.error,
    //       ]);
    //     },
    //   });
  }
  async onSearchChangedAccount({ search, page, clear = false }: { search: string; page: number, clear: boolean }) {
    this.isDropdownLoading = true;
    this.listingOptionAccount.search = search;
    this.listingOptionAccount.pageNumber = page;
    if (clear) {
      this.accounts = [];
    }
    this.accountService.dropdown(this.listingOptionAccount).subscribe(it => {
      this.hasMoreAccount = page * it.totalCount < it.totalPages * it.totalCount;
      this.accounts = clear
      ? it.data
      : [...this.accounts, ...it.data];
      this.isDropdownLoading = false;
    })
  }
  async onSearchChangedCategory({ search, page, clear = false }: { search: string; page: number, clear: boolean }) {
    this.isDropdownLoading = true;
    this.listingOptionCategory.search = search;
    this.listingOptionCategory.pageNumber = page;
    if (clear) {
      this.categories = [];
    }
    this.categoryService.dropdown(this.listingOptionCategory).subscribe(it => {
      this.hasMoreAccount = page * it.totalCount < it.totalPages * it.totalCount;
      this.categories = clear
      ? it.data
      : [...this.categories, ...it.data];
      this.isDropdownLoading = false;
    })
  }

  onSelectionChangeAccount(selected:AccountFragment[]): void {
    this.listingOptionAccount.exclude = selected.map(it => it.name).join(",");
    //  = selected.map(it => it.name).join(",");
    // this.refresh();
  }
  onSelectionChangeCategory(selected:CategoryFragment[]): void {
    this.listingOptionCategory.exclude = selected.map(it => it.name).join(",");
    //  = selected.map(it => it.name).join(",");
    // this.refresh();
  }
}
