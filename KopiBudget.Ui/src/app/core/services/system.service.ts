import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ApiResult, GenericListingResult, NullApiResult } from '../../domain/models/api-result';
import { CurrencyDto } from '../../domain/models/currency';
import { ModuleGroupResponse } from '../../domain/models/module';
import { PersonalCategoryFieldDto } from '../../domain/models/personal-category';
import { mapItemsToGenericListing } from '../generics/listing-result.mapper.ts';
import { GenericService } from './generic.service';

@Injectable({
	providedIn: 'root',
})
export class SystemService extends GenericService {
	private controller = '/system';
	constructor(private httpClient: HttpClient) {
		super(httpClient);
	}
	getModuleGroups(): Observable<ApiResult<ModuleGroupResponse[]>> {
		return this.get(`${this.controller}/GetModuleGroups/`, null, false);
	}
	getCurrencies(): Observable<ApiResult<CurrencyDto[]>> {
		return this.get(`${this.controller}/GetCurrencies/`, null, false);
	}
	dropdownCurrency(dropdownListingOption): Observable<GenericListingResult<CurrencyDto[]>> {
		const queryParams = this.setQueryParameters(dropdownListingOption);
		return this.get<any>(`${this.controller}/DropdownCurrency?${queryParams}`, null, true).pipe(
			map((res) => mapItemsToGenericListing<CurrencyDto[]>(res.data)),
		);
	}
	getPersonalCategoryFields(): Observable<ApiResult<PersonalCategoryFieldDto>> {
		return this.get(`${this.controller}/GetPersonalCategoryFields/`, null, false);
	}
	updateCurrency(payload): Observable<NullApiResult> {
		return this.put<NullApiResult>(
			`${this.controller}/UpdateCurrency`,
			payload,
			this.getAuthorizationHeader(),
		);
	}
}
