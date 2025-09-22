import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router } from '@angular/router';
import { PermissionService } from '../services/permission.service';
import { ToastService } from '../services/toast.service';
import { AuthService } from './auth.service';

@Injectable({
	providedIn: 'root',
})
export class AuthGuard {
	constructor(
		private auth: AuthService,
		private router: Router,
		private permissionService: PermissionService,
		private toastService: ToastService,
	) {}

	canActivate(route: ActivatedRouteSnapshot): boolean {
		if (this.auth.isAuthenticated()) {
			const requiredPermissions = route.data['permission']?.split(',');
			let exists = false;
			if (requiredPermissions && requiredPermissions.length > 0) {
				requiredPermissions.forEach((it) => {
					if (it != '') {
						exists = this.permissionService.hasPermission(
							it.split(':')[0],
							it.split(':')[1],
						);
					}
				});
			}
			if (!exists) {
				this.toastService.error('Permission denied.');
				this.router.navigate(['/admin']);
			}
			return exists;
		}
		this.toastService.error('Token invalid or expired.');
		this.router.navigate(['/login']);
		return false;
	}
}
