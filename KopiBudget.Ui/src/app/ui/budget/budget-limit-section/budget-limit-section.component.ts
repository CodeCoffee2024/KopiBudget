import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { finalize, switchMap } from 'rxjs';
import { BudgetPersonalCategoryService } from '../../../core/services/budget-personal-category.service';
import { BudgetService } from '../../../core/services/budget.service';
import { LoadingService } from '../../../core/services/loading.service';
import { ModalService } from '../../../core/services/modal.service';
import { PersonalCategoryService } from '../../../core/services/personal-category.service';
import { ToastService } from '../../../core/services/toast.service';
import { BudgetDto } from '../../../domain/models/budget';
import { BudgetPersonalCategoryConstants } from '../../../domain/models/budget-personal-category';
import { ExchangeRateDto } from '../../../domain/models/exchange-rate';
import { ToastType } from '../../../domain/models/toast';
import { BudgetPersonalCategoryCreateComponent } from '../budget-personal-category-create/budget-personal-category-create.component';
import { BudgetPersonalCategoryLimitUpdateComponent } from '../budget-personal-category-limit-update/budget-personal-category-limit-update.component';

@Component({
	selector: 'app-budget-limit-section',
	standalone: true,
	imports: [CommonModule],
	templateUrl: './budget-limit-section.component.html',
	styleUrls: ['./budget-limit-section.component.scss'],
})
export class BudgetLimitSectionComponent implements OnChanges {
	@Input() exchangeRate: ExchangeRateDto;
	@Input() budgets: BudgetDto[] = [];
	selectedBudget: BudgetDto; // default
	isLoading = false;
	@Output() refreshData: EventEmitter<boolean> = new EventEmitter<boolean>();

	constructor(
		private modalService: ModalService,
		private personalCategoryService: PersonalCategoryService,
		private budgetService: BudgetService,
		private loadingService: LoadingService,
		private budgetPersonalCategoryService: BudgetPersonalCategoryService,
		private toastService: ToastService,
	) {}
	ngOnInit(): void {
		this.selectedBudget = this.budgets[0]; // default
	}

	ngOnChanges(changes: SimpleChanges): void {
		if (changes['budgets'] && changes['budgets'].currentValue) {
			// reset selectedBudget when budgets array is refreshed
			const newBudgets: BudgetDto[] = changes['budgets'].currentValue;
			if (newBudgets.length > 0) {
				this.selectedBudget =
					newBudgets.find((it) => it.id == this.selectedBudget?.id) ?? newBudgets[0];
			} else {
				this.selectedBudget = null;
			}
		}
	}
	onSelectBudget(budget: any) {
		this.selectedBudget = budget;
	}
	async onEdit(category) {
		this.personalCategoryService
			.getAll()
			.pipe(
				finalize(() => {
					this.isLoading = false;
					this.loadingService.hide();
				}),
			)
			.subscribe({
				next: async (data) => {
					const result = await this.modalService.open(
						BudgetPersonalCategoryLimitUpdateComponent,
						{
							personalCategories: data.data,
							budget: this.selectedBudget,
							budgetPersonalCategory: category,
							id: this.selectedBudget.id,
						},
					);
					if (result) {
						this.toastService.success(
							'Success',
							BudgetPersonalCategoryConstants.UPDATESUCCESS,
						);
						this.refreshData.emit(true);
					}
				},
			});
	}
	onDelete(category) {
		this.toastService
			.confirm(ToastType.CONFIRMATION, BudgetPersonalCategoryConstants.DELETECONFIRMATION)
			.then((it) => {
				if (it) {
					this.isLoading = true;
					this.loadingService.show();
					this.budgetPersonalCategoryService
						.remove(this.selectedBudget.id, category.personalCategory.id)
						.subscribe(() => {
							this.toastService.success(
								'Success',
								BudgetPersonalCategoryConstants.DELETESUCCESS,
							);
							this.refreshData.emit(true);
						});
				}
			});
	}
	addLimit() {
		this.loadingService.show();

		this.budgetService
			.getBudget(this.selectedBudget.id)
			.pipe(
				switchMap((budgetRes) => {
					// Stop early if no unallocated amount
					if (budgetRes.data.unallocatedLimitAmount <= 0) {
						return []; // return EMPTY observable
					}

					return this.personalCategoryService.getAll().pipe(
						switchMap(async (personalCategoriesRes) => {
							const result = await this.modalService.open(
								BudgetPersonalCategoryCreateComponent,
								{
									personalCategories: personalCategoriesRes.data,
									budget: this.selectedBudget,
								},
							);

							if (result) {
								this.toastService.success(
									'Success',
									BudgetPersonalCategoryConstants.CREATESUCCESS,
								);
								this.refreshData.emit(true);
							}
						}),
					);
				}),
				finalize(() => this.loadingService.hide()),
			)
			.subscribe({
				error: () => {
					this.toastService.error('Error', 'Something went wrong.');
				},
			});
	}
}
