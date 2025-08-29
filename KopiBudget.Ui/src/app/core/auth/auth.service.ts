import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ApiResult } from '../../domain/models/api-result';
import { GenericService } from '../services/generic.service';
import { AuthUser, LoginResponse } from './auth.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends GenericService {
  private readonly tokenKey = 'access_token';
  private readonly userKey = 'auth_user';
	private controller = 'auth/';

  private currentUserSubject = new BehaviorSubject<AuthUser | null>(this.getStoredUser());
  currentUser$ = this.currentUserSubject.asObservable();

	constructor(private httpClient: HttpClient) {
		super(httpClient);
	}

	login(payload): Observable<ApiResult<LoginResponse>> {
		const queryParams = this.setQueryParameters(payload);
		return this.get(
			`/${this.controller}login?` + queryParams,
			null,
			false
		);
	}
  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.userKey);
    this.currentUserSubject.next(null);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem(this.tokenKey);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  private setSession(response: LoginResponse) {
    localStorage.setItem(this.tokenKey, response.token);
    // this.currentUserSubject.next(user);
  }

  private getStoredUser(): AuthUser | null {
    const data = localStorage.getItem(this.userKey);
    return data ? JSON.parse(data) : null;
  }
}
