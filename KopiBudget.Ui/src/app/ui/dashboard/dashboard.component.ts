import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { finalize, forkJoin } from 'rxjs';
import { AuthService } from '../../core/auth/auth.service';
import { BudgetService } from '../../core/services/budget.service';
import { DashboardService } from '../../core/services/dashboard.service';
import { ExchangeRateService } from '../../core/services/exchange-rate.service';
import { LoadingService } from '../../core/services/loading.service';
import { ModalService } from '../../core/services/modal.service';
import { ToastService } from '../../core/services/toast.service';
import { BudgetDto } from '../../domain/models/budget';
import { DashboardSummaryDto } from '../../domain/models/dashboard';
import { ExchangeRateDto } from '../../domain/models/exchange-rate';
import { TransactionTypes } from '../../domain/models/transaction';
import { BudgetSectionComponent } from '../budget/budget-section/budget-section.component';
import { ChartComponent } from '../shared/components/chart/chart.component';
import { DiffBadgeComponent } from '../shared/components/diff-badge/diff-badge.component';

@Component({
	selector: 'app-dashboard',
	standalone: true,
	imports: [CommonModule, ChartComponent, BudgetSectionComponent, DiffBadgeComponent],
	templateUrl: './dashboard.component.html',
	styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
	activeTab = TransactionTypes.ACCOUNT;
	TransactionTypes = TransactionTypes;
	isLoading = false;
	expensesPerCategory: DashboardSummaryDto[];
	expensesPerPersonalCategory: DashboardSummaryDto[];
	balance = 0;
	expense = 0;
	balancePrevious = 0;
	expensePrevious = 0;
	budgets: BudgetDto[] = [];
	exchangeRate: ExchangeRateDto;
	colorsUsedCategory = [];
	colorsUsedPersonal = [];
	constructor(
		private dashboardService: DashboardService,
		private modalService: ModalService,
		private authService: AuthService,
		private loadingService: LoadingService,
		private budgetService: BudgetService,
		private exchangeRateService: ExchangeRateService,
		private toastService: ToastService,
	) {}
	ngOnInit(): void {
		this.loadingService.show();
		console.log(this.authService.user);
		forkJoin({
			balanceExpenses: this.dashboardService.GetDashboardBalanceExpenses(),
			personalCategory: this.dashboardService.GetDashboardExpensesPerPersonalCategory(),
			category: this.dashboardService.GetDashboardExpensesPerCategory(),
			rates: this.exchangeRateService.getAll(),
			budgets: this.budgetService.getAll(),
		})
			.pipe(
				finalize(() => {
					this.isLoading = false;
					this.loadingService.hide();
				}),
			)
			.subscribe({
				next: (res) => {
					this.expensesPerCategory = res.category.data;
					this.expensesPerPersonalCategory = res.personalCategory.data;
					this.balance = Number(res.balanceExpenses.data[0].value);
					this.expense = Number(res.balanceExpenses.data[1].value);
					this.balancePrevious = Number(res.balanceExpenses.data[0].previousMonth);
					this.expensePrevious = Number(res.balanceExpenses.data[1].previousMonth);
					this.exchangeRate = res.rates.data;
					this.budgets = res.budgets.data;
				},
				error: () => {},
			});
	}
	get firstName() {
		return this.authService.user.firstName;
	}
	get getPersonalCategoryValues() {
		return this.expensesPerPersonalCategory?.map((it) => it.value);
	}
	get getPersonalCategoryLabels() {
		return this.expensesPerPersonalCategory?.map((it) => it.label);
	}
	colors(colors, type) {
		if (type == TransactionTypes.BUDGET) {
			this.colorsUsedPersonal = colors;
		} else {
			this.colorsUsedCategory = colors;
		}
	}
	setTab(tab) {
		this.activeTab = tab;
	}
}
