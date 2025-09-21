import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ApiResult, GenericListingResult, NullApiResult } from '../../domain/models/api-result';
import { TransactionDto } from '../../domain/models/transaction';
import { mapItemsToGenericListing } from '../generics/listing-result.mapper.ts';
import { GenericService } from './generic.service';

@Injectable({
  providedIn: 'root',
})
export class TransactionService extends GenericService {
  private controller = '/transaction/';
  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }
  create(payload): Observable<ApiResult<TransactionDto>> {
    return this.post<ApiResult<TransactionDto>>(
      `${this.controller}`,
      payload,
      this.getAuthorizationHeader(),
    );
  }
  update(id, payload): Observable<ApiResult<TransactionDto>> {
    return this.put<ApiResult<TransactionDto>>(
      `${this.controller}${id}`,
      payload,
      this.getAuthorizationHeader(),
    );
  }
  remove(id): Observable<NullApiResult> {
    return this.delete<NullApiResult>(`${this.controller}${id}`);
  }

  getTransactions(listingOption): Observable<GenericListingResult<TransactionDto[]>> {
    const queryParams = this.setQueryParameters(listingOption);

    return this.get<any>(`${this.controller}Transactions?${queryParams}`, null, true).pipe(
      map((res) => mapItemsToGenericListing<TransactionDto[]>(res.data)),
    );
  }
  getTransaction(id): Observable<ApiResult<TransactionDto>> {
    return this.get(`${this.controller}${id}`, null, true);
  }
}
