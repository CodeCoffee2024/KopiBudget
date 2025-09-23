import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResult } from '../../domain/models/api-result';
import { DashboardBalanceExpenseDto, DashboardSummaryDto } from '../../domain/models/dashboard';
import { GenericService } from './generic.service';

@Injectable({
	providedIn: 'root',
})
export class DashboardService extends GenericService {
	private controller = '/dashboard/';
	constructor(private httpClient: HttpClient) {
		super(httpClient);
	}
	GetDashboardBalanceExpenses(): Observable<ApiResult<DashboardBalanceExpenseDto[]>> {
		return this.get<ApiResult<DashboardBalanceExpenseDto[]>>(
			`${this.controller}GetDashboardBalanceExpenses`,
			null,
			true,
		);
	}
	GetDashboardExpensesPerCategory(): Observable<ApiResult<DashboardSummaryDto[]>> {
		return this.get<ApiResult<DashboardBalanceExpenseDto[]>>(
			`${this.controller}GetDashboardExpensesPerCategory`,
			null,
			true,
		);
	}
	GetDashboardExpensesPerPersonalCategory(): Observable<ApiResult<DashboardSummaryDto[]>> {
		return this.get<ApiResult<DashboardBalanceExpenseDto[]>>(
			`${this.controller}GetDashboardExpensesPerPersonalCategory`,
			null,
			true,
		);
	}
}
