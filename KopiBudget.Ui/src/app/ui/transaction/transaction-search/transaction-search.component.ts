import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { Observable } from 'rxjs';
import { AccountService } from '../../../core/services/account.service';
import { BudgetService } from '../../../core/services/budget.service';
import { CategoryService } from '../../../core/services/category.service';
import { PersonalCategoryService } from '../../../core/services/personal-category.service';
import { AccountFragment } from '../../../domain/models/account';
import { BudgetFragment } from '../../../domain/models/budget';
import { CategoryFragment } from '../../../domain/models/category';
import { InputTypes } from '../../../domain/models/input-type';
import { GenericDropdownListingOption } from '../../../domain/models/listing-option';
import { PersonalCategoryDropdownListingOption, PersonalCategoryFragment } from '../../../domain/models/personal-category';
import { Transaction, TransactionTypes } from '../../../domain/models/transaction';
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
  @Input() type = TransactionTypes.ACCOUNT;
  TransactionTypes = TransactionTypes;
  @Output() refresh = new EventEmitter<object>();

  InputTypes = InputTypes;

  isDropdownLoading = false;

  // Dropdown data
  categories: CategoryFragment[] = [];
  accounts: AccountFragment[] = [];
  budgets: BudgetFragment[] = [];
  personalCategories: PersonalCategoryFragment[] = [];

  // Pagination states
  hasMoreCategory = false;
  hasMoreAccount = false;
  hasMoreBudget = false;
  hasMorePersonalCategory = false;

  // Listing options
  listingOptionAccount = new GenericDropdownListingOption();
  listingOptionCategory = new GenericDropdownListingOption();
  listingOptionBudget = new GenericDropdownListingOption();
  listingOptionPersonalCategory = new PersonalCategoryDropdownListingOption();

  constructor(
    private categoryService: CategoryService,
    private accountService: AccountService,
    private budgetService: BudgetService,
    private personalCategoryService: PersonalCategoryService,
  ) {}

  onSubmit(): void {
    this.refresh.emit(this.Transaction.toSubmitSearchForm);
  }
  private loadDropdown<T>(
    service: { dropdown: (opt: GenericDropdownListingOption | PersonalCategoryDropdownListingOption) => Observable<{ data: T[]; totalCount: number; totalPages: number }> },
    option: GenericDropdownListingOption | PersonalCategoryDropdownListingOption,
    state: { items: T[]; set: (items: T[]) => void; hasMore: (val: boolean) => void },
    { search, page, clear }: { search: string; page: number; clear: boolean }
  ) {
    this.isDropdownLoading = true;
    option.search = search;
    option.pageNumber = page;

    if (clear) state.set([]);

    service.dropdown(option).subscribe({
      next: (it) => {
        state.hasMore(page * it.totalCount < it.totalPages * it.totalCount);
        state.set(clear ? it.data : [...state.items, ...it.data]);
        this.isDropdownLoading = false;
      },
      error: () => (this.isDropdownLoading = false),
    });
  }

  onSearchChangedAccount(e: { search: string; page: number; clear: boolean }) {
    if (this.type !== TransactionTypes.ACCOUNT) return;
    this.loadDropdown<AccountFragment>(
      this.accountService,
      this.listingOptionAccount,
      {
        items: this.accounts,
        set: (val) => (this.accounts = val),
        hasMore: (val) => (this.hasMoreAccount = val),
      },
      e
    );
  }

  onSearchChangedCategory(e: { search: string; page: number; clear: boolean }) {
    if (this.type !== TransactionTypes.ACCOUNT) return;
    this.loadDropdown<CategoryFragment>(
      this.categoryService,
      this.listingOptionCategory,
      {
        items: this.categories,
        set: (val) => (this.categories = val),
        hasMore: (val) => (this.hasMoreCategory = val),
      },
      e
    );
  }

  onSearchChangedBudget(e: { search: string; page: number; clear: boolean }) {
    if (this.type !== TransactionTypes.BUDGET) return;
    this.loadDropdown<BudgetFragment>(
      this.budgetService,
      this.listingOptionBudget,
      {
        items: this.budgets,
        set: (val) => (this.budgets = val),
        hasMore: (val) => (this.hasMoreBudget = val),
      },
      e
    );
  }

  onSearchChangedPersonalCategory(e: { search: string; page: number; clear: boolean }) {
    if (this.type !== TransactionTypes.BUDGET) return;
    this.listingOptionPersonalCategory.budgetIds = this.listingOptionBudget.exclude;
    this.loadDropdown<PersonalCategoryFragment>(
      this.personalCategoryService,
      this.listingOptionPersonalCategory,
      {
        items: this.personalCategories,
        set: (val) => (this.personalCategories = val),
        hasMore: (val) => (this.hasMorePersonalCategory = val),
      },
      e
    );
  }

  onSelectionChangeAccount(selected: AccountFragment[]): void {
    if (this.type !== TransactionTypes.ACCOUNT) return;
    this.listingOptionAccount.exclude = selected.map((it) => it.id).join(',');
    this.Transaction.searchForm.get('accountIds')?.setValue(this.listingOptionAccount.exclude);
  }

  onSelectionChangeCategory(selected: CategoryFragment[]): void {
    if (this.type !== TransactionTypes.ACCOUNT) return;
    this.listingOptionCategory.exclude = selected.map((it) => it.id).join(',');
    this.Transaction.searchForm.get('categoryIds')?.setValue(this.listingOptionCategory.exclude);
  }

  onSelectionChangeBudget(selected: BudgetFragment[]): void {
    if (this.type !== TransactionTypes.BUDGET) return;
    this.listingOptionBudget.exclude = selected.map((it) => it.id).join(',');
    this.Transaction.searchForm.get('budgetIds')?.setValue(this.listingOptionBudget.exclude);
  }

  onSelectionChangePersonalCategory(selected: PersonalCategoryFragment[]): void {
    if (this.type !== TransactionTypes.BUDGET) return;
    this.listingOptionPersonalCategory.exclude = selected.map((it) => it.id).join(',');
    this.Transaction.searchForm.get('personalCategoryIds')?.setValue(this.listingOptionPersonalCategory.exclude);
  }
}

