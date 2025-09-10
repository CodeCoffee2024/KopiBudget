import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResult } from '../../domain/models/api-result';
import { TransactionDto } from '../../domain/models/transaction';
import { GenericService } from './generic.service';

@Injectable({
  providedIn: 'root'
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
      this.getAuthorizationHeader()
    );
  }
  getTransactions(): Observable<ApiResult<TransactionDto[]>> {
    return this.get<ApiResult<TransactionDto[]>>(
      `${this.controller}Transactions`,
      null,
      true
    );
  }
}

