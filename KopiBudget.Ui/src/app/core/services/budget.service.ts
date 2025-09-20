import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ApiResult, GenericListingResult, NullApiResult } from '../../domain/models/api-result';
import { BudgetDto, BudgetFragment } from '../../domain/models/budget';
import { mapItemsToGenericListing } from '../generics/listing-result.mapper.ts';
import { GenericService } from './generic.service';

@Injectable({
  providedIn: 'root'
})
export class BudgetService extends GenericService {
  private controller = '/budget/';
  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }
  create(payload): Observable<ApiResult<BudgetDto>> {
    return this.post<ApiResult<BudgetDto>>(
    `${this.controller}`,
    payload,
    this.getAuthorizationHeader()
    );
  }
  update(id, payload): Observable<ApiResult<BudgetDto>> {
    return this.put<ApiResult<BudgetDto>>(
    `${this.controller}${id}`,
    payload,
    this.getAuthorizationHeader()
    );
  }
  remove(id): Observable<NullApiResult> {
    return this.delete<NullApiResult>(
    `${this.controller}${id}`
    );
  }

  getBudgets(
    listingOption
  ): Observable<GenericListingResult<BudgetDto[]>> {
    const queryParams = this.setQueryParameters(listingOption);

    return this.get<any>(
      `${this.controller}Budgets?${queryParams}`,
      null,
      true
    ).pipe(
      map((res) =>
        mapItemsToGenericListing<BudgetDto[]>(res.data)
      )
    );
  }
  getBudget(id): Observable<ApiResult<BudgetDto>> {
    return this.get(`${this.controller}${id}`, null, true);
  }

  getAll(): Observable<ApiResult<BudgetDto[]>> {
    return this.get<any>(
      `${this.controller}`,
      null,
      true
    );
  }
  dropdown(
    dropdownListingOption
  ): Observable<GenericListingResult<BudgetFragment[]>> {
    const queryParams = this.setQueryParameters(
      dropdownListingOption
    );
    return this.get<any>(
      `${this.controller}Dropdown?${queryParams}`,
      null,
      true
    ).pipe(
      map((res) =>
        mapItemsToGenericListing<BudgetFragment[]>(
          res.data
        )
      )
    );
  }
}

