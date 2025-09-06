import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountDto } from '../../domain/models/account';
import { ApiResult } from '../../domain/models/api-result';
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
}
