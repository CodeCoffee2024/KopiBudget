import { CommonModule } from '@angular/common';
import {
	Component,
	EventEmitter,
	Input,
	OnChanges,
	OnInit,
	Output,
	SimpleChanges,
} from '@angular/core';
import { finalize } from 'rxjs';
import { BudgetService } from '../../../core/services/budget.service';
import { LoadingService } from '../../../core/services/loading.service';
import { ModalService } from '../../../core/services/modal.service';
import { PersonalCategoryService } from '../../../core/services/personal-category.service';
import { ToastService } from '../../../core/services/toast.service';
import { BudgetConstants, BudgetDto } from '../../../domain/models/budget';
import { ExchangeRateDto } from '../../../domain/models/exchange-rate';
import { ToastType } from '../../../domain/models/toast';
import { BudgetUpdateComponent } from '../budget-update/budget-update.component';

@Component({
	selector: 'app-budget-section',
	standalone: true,
	imports: [CommonModule],
	templateUrl: './budget-section.component.html',
	styleUrls: ['./budget-section.component.scss'],
})
export class BudgetSectionComponent implements OnInit, OnChanges {
	@Input() exchangeRate: ExchangeRateDto;
	@Input() budgets: BudgetDto[] = [];
	@Input() hideOptions = false;
	selectedBudget: BudgetDto; // default
	isLoading = false;
	@Output() refreshData: EventEmitter<boolean> = new EventEmitter<boolean>();
	constructor(
		private modalService: ModalService,
		private budgetService: BudgetService,
		private personalCategoryService: PersonalCategoryService,
		private toastService: ToastService,
		private loadingService: LoadingService,
	) {}
	ngOnInit(): void {
		console.log(this.budgets);
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
	async onEdit() {
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
					const result = await this.modalService.open(BudgetUpdateComponent, {
						personalCategories: data.data,
						budget: this.selectedBudget,
					});
					if (result) {
						this.toastService.success('Success', BudgetConstants.UPDATESUCCESS);
						this.refreshData.emit(true);
					}
				},
			});
	}
	onDelete() {
		this.toastService
			.confirm(ToastType.CONFIRMATION, BudgetConstants.DELETECONFIRMATION)
			.then((it) => {
				if (it) {
					this.isLoading = true;
					this.loadingService.show();
					this.budgetService
						.remove(this.selectedBudget.id)
						.pipe(
							finalize(() => {
								this.isLoading = false;
								this.loadingService.hide();
							}),
						)
						.subscribe({
							next: async (data) => {
								this.toastService.success('Success', BudgetConstants.DELETESUCCESS);
								this.refreshData.emit(true);
							},
						});
				}
			});
	}
}
