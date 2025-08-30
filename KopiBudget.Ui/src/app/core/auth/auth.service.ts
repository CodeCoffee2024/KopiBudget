import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { ApiResult } from '../../domain/models/api-result';
import { GenericService } from '../services/generic.service';
import { AuthUser, LoginResponse } from './auth.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends GenericService {
  private readonly tokenKey = 'token';
  private readonly refreshTokenKey = 'refreshToken';
  private readonly userKey = 'user';
	private controller = 'auth/';

  private currentUserSubject = new BehaviorSubject<AuthUser | null>(this.getStoredUser());
  currentUser$ = this.currentUserSubject.asObservable();

	constructor(private httpClient: HttpClient) {
		super(httpClient);
	}

	login(payload): Observable<ApiResult<LoginResponse>> {
		return this.post(
			`/${this.controller}login`,payload,
			null
		);
	}
  getRefreshToken(): string | null {
    return localStorage.getItem(this.refreshTokenKey);
  }
  refreshToken(): Observable<LoginResponse> {
    const refreshToken = this.getRefreshToken();

    return this.post<ApiResult<LoginResponse>>(
        `/${this.controller}refresh`,
        { "refreshToken": refreshToken }
      ).pipe(
        map(response => {
          const loginResponse = response.data;
          console.log(loginResponse)
          if (loginResponse) {
            this.setSession(loginResponse);
          }

          return loginResponse;
        })
      );
  }
  logout(): void {
    localStorage.removeItem(this.tokenKey);
    // localStorage.removeItem(this.userKey);
    this.currentUserSubject.next(null);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem(this.tokenKey);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  setSession(response: LoginResponse) {
    localStorage.setItem(this.tokenKey, response.token);
    localStorage.setItem(this.refreshTokenKey, response.refreshToken);
    // this.currentUserSubject.next(user);
  }

  private getStoredUser(): AuthUser | null {
    const data = localStorage.getItem(this.userKey);
    return data ? JSON.parse(data) : null;
  }
}
