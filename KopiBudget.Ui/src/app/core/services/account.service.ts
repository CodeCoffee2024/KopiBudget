import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { AccountDto, AccountFragment } from '../../domain/models/account';
import { ApiResult, GenericListingResult } from '../../domain/models/api-result';
import { mapItemsToGenericListing } from '../generics/listing-result.mapper.ts';
import { GenericService } from './generic.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService extends GenericService {
  private controller = '/account/';
  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }
  create(payload): Observable<ApiResult<AccountDto>> {
    return this.post<ApiResult<AccountDto>>(
      `${this.controller}`,
      payload,
      this.getAuthorizationHeader()
    );
  }
  getUserAccounts(): Observable<ApiResult<AccountDto[]>> {
    return this.get<ApiResult<AccountDto[]>>(
      `${this.controller}UserAccounts`,
      null,
      true
    );
  }
  dropdown(
    dropdownListingOption
  ): Observable<GenericListingResult<AccountFragment[]>> {
    const queryParams = this.setQueryParameters(
      dropdownListingOption
    );
    return this.get<any>(
      `${this.controller}Dropdown?${queryParams}`,
      null,
      true
    ).pipe(
      map((res) =>
        mapItemsToGenericListing<AccountFragment[]>(
          res.data
        )
      )
    );
  }
}
