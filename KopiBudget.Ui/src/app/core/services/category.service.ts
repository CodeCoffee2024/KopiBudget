import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ApiResult, GenericListingResult } from '../../domain/models/api-result';
import { CategoryFragment } from '../../domain/models/category';
import { mapItemsToGenericListing } from '../generics/listing-result.mapper.ts';
import { GenericService } from './generic.service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService extends GenericService {
	private controller = '/category/';
	constructor(private httpClient: HttpClient) {
		super(httpClient);
	}
	create(payload): Observable<ApiResult<CategoryFragment>> {
		return this.post<ApiResult<CategoryFragment>>(
			`${this.controller}`,
			payload,
			this.getAuthorizationHeader()
		);
	}
	dropdown(
		dropdownListingOption
	): Observable<GenericListingResult<CategoryFragment[]>> {
		const queryParams = this.setQueryParameters(
			dropdownListingOption
		);
		return this.get<any>(
			`${this.controller}Dropdown?${queryParams}`,
			null,
			true
		).pipe(
			map((res) =>
				mapItemsToGenericListing<CategoryFragment[]>(
					res.data
				)
			)
		);
	}
}
