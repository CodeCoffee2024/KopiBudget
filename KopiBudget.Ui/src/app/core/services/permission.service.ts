import { Injectable } from '@angular/core';
import { AuthUser } from '../auth/auth.model';

@Injectable({
	providedIn: 'root',
})
export class PermissionService {
	get permissions() {
		return (JSON.parse(localStorage.getItem('user')) as AuthUser).permissions;
	}
	hasPermission(module, permission) {
		return this.permissions.find((it) => it.module == module && it.name == permission) != null;
	}
}
