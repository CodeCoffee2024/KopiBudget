import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GenericService {
	private baseUrl = environment.apiUrl;
	constructor(private http: HttpClient) {}

	protected get<T>(
		endpoint: string,
		params?: HttpParams,
		useAuthorizationHeader = false
	): Observable<T> {
		const headers = useAuthorizationHeader
			? this.getAuthorizationHeader()
			: undefined;
		return this.http.get<T>(`${this.baseUrl}${endpoint}`, {
			params,
			headers,
		});
	}
	setQueryParameters(queryParams: Record<string, any>): string {
		return Object.keys(queryParams)
			.filter(param => queryParams[param] !== null && queryParams[param] !== undefined && queryParams[param] !== "")
			.map(param => {
			const value = queryParams[param];

			if (Array.isArray(value)) {
				return value.map(v => `${param}=${encodeURIComponent(v)}`).join("&");
			}

			return `${param}=${encodeURIComponent(value)}`;
			})
			.join("&");
	}
	getFile(
		endpoint: string,
		params?: HttpParams,
		useAuthorizationHeader = false
	): Observable<Blob> {
		const headers = useAuthorizationHeader
			? this.getAuthorizationHeader()
			: undefined;
		return this.http.get(`${this.baseUrl}${endpoint}`, {
			params,
			headers,
			responseType: 'blob',
		});
	}
	getAuthorizationHeader(): HttpHeaders {
		return new HttpHeaders({
			Authorization: `Bearer ${localStorage.getItem(
				'token'
			)}`,
		});
	}

	protected post<T>(
		endpoint: string,
		payload: any,
		headers?: HttpHeaders
	): Observable<T> {
		return this.http.post<T>(
			`${this.baseUrl}${endpoint}`,
			payload,
			{ headers }
		);
	}

	protected put<T>(
		endpoint: string,
		payload: any,
		headers?: HttpHeaders
	): Observable<T> {
		return this.http.put<T>(
			`${this.baseUrl}${endpoint}`,
			payload,
			{ headers }
		);
	}

	protected delete<T>(
		endpoint: string,
		params?: HttpParams
	): Observable<T> {
		return this.http.delete<T>(
			`${this.baseUrl}${endpoint}`,
			{ params, headers: this.getAuthorizationHeader() }
		);
	}
}
