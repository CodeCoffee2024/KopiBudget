import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, switchMap, throwError } from 'rxjs';
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

  login(payload): Observable<AuthUser> {
    return this.post<ApiResult<LoginResponse>>(
      `/${this.controller}login`,
      payload,
      null
    ).pipe(
      switchMap(response => {
        const loginResponse = response.data;

        if (loginResponse) {
          this.setSession(loginResponse);

          return this.get<ApiResult<AuthUser>>(
            `/${this.controller}user-detail`,null, true
          ).pipe(
            map(userResponse => {
              this.setUserData(userResponse.data);
              return userResponse.data;
            })
          );
        }

        return throwError(() => new Error('Login failed'));
      })
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
          if (loginResponse) {
            this.setSession(loginResponse);
          }

          return loginResponse;
        })
      );
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.userKey);
    localStorage.removeItem(this.refreshTokenKey);
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
  }

  setUserData(response: AuthUser) {
    localStorage.setItem(this.userKey, JSON.stringify(response));
    this.currentUserSubject.next(response);
  }

  private getStoredUser(): AuthUser | null {
    const data = localStorage.getItem(this.userKey);
    return data ? JSON.parse(data) : null;
  }
}
