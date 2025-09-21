import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResult, NullApiResult } from '../../domain/models/api-result';
import { BudgetPersonalCategoryFragment } from '../../domain/models/budget-personal-category';
import { GenericService } from './generic.service';

@Injectable({
	providedIn: 'root',
})
export class BudgetPersonalCategoryService extends GenericService {
	private controller = '/budgetPersonalCategory/';
	constructor(private httpClient: HttpClient) {
		super(httpClient);
	}
	create(payload): Observable<ApiResult<BudgetPersonalCategoryFragment>> {
		return this.post<ApiResult<BudgetPersonalCategoryFragment>>(
			`${this.controller}`,
			payload,
			this.getAuthorizationHeader(),
		);
	}
	update(
		budgetId,
		personalCategoryId,
		payload,
	): Observable<ApiResult<BudgetPersonalCategoryFragment>> {
		return this.put<ApiResult<BudgetPersonalCategoryFragment>>(
			`${this.controller}${budgetId}/${personalCategoryId}`,
			payload,
			this.getAuthorizationHeader(),
		);
	}
	remove(budgetId, personalCategoryId): Observable<NullApiResult> {
		return this.delete<NullApiResult>(`${this.controller}${budgetId}/${personalCategoryId}`);
	}
}
