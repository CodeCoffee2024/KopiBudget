import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResult } from '../../domain/models/api-result';
import { ModuleGroupResponse } from '../../domain/models/module';
import { GenericService } from './generic.service';

@Injectable({
  providedIn: 'root'
})
export class SystemService extends GenericService {
	private controller = '/system';
	constructor(private httpClient: HttpClient) {
		super(httpClient);
	}
	getModuleGroups(): Observable<
		ApiResult<ModuleGroupResponse[]>
	> {
		return this.get(
			`${this.controller}/GetModuleGroups/`,
			null,
			false
		);
	}
}
