import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResult } from '../../domain/models/api-result';
import { UserDto } from '../../domain/models/user';
import { GenericService } from './generic.service';

@Injectable({
  providedIn: 'root',
})
export class UserService extends GenericService {
  private controller = '/user/';
  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }
  register(payload): Observable<ApiResult<UserDto>> {
    return this.post<ApiResult<UserDto>>(
      `${this.controller}register`,
      payload,
      this.getAuthorizationHeader(),
    );
  }
}
