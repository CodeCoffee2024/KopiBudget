import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResult } from '../../domain/models/api-result';
import { ExchangeRateDto } from '../../domain/models/exchange-rate';
import { GenericService } from './generic.service';

@Injectable({
  providedIn: 'root',
})
export class ExchangeRateService extends GenericService {
  private controller = '/exchangeRates/';
  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }
  getAll(): Observable<ApiResult<ExchangeRateDto>> {
    return this.get<ApiResult<ExchangeRateDto>>(`${this.controller}`, null, true);
  }
}
