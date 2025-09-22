import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { finalize, forkJoin } from 'rxjs';
import { LoadingService } from '../../../../core/services/loading.service';
import { PermissionService } from '../../../../core/services/permission.service';
import { SystemService } from '../../../../core/services/system.service';
import { ModuleGroupResponse } from '../../../../domain/models/module';

@Component({
	selector: 'app-sidebar',
	standalone: true,
	imports: [CommonModule],
	templateUrl: './sidebar.component.html',
	styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent {
	@Input() isSidebarOpen = false;
	@Input() isDesktop = false;
	modules: ModuleGroupResponse[];
	isLoading = true;
	@Output() closeSidebar = new EventEmitter<void>();
	constructor(
		private systemService: SystemService,
		private loadingService: LoadingService,
		private permissionService: PermissionService,
		private route: Router,
	) {}

	ngOnInit(): void {
		this.loadingService.show();
		this.isLoading = true;

		forkJoin({
			modules: this.systemService.getModuleGroups(),
			currencies: this.systemService.getCurrencies(),
		})
			.pipe(
				finalize(() => {
					this.loadingService.hide();
					this.isLoading = false;
				}),
			)
			.subscribe({
				next: (res) => {
					this.modules = res.modules.data;
					let matchingModules = [];
					this.modules.forEach((group) => {
						group.modules.forEach((module) => {
							if (
								this.permissionService.permissions.find(
									(it) => it.module == module.name,
								) &&
								!matchingModules.find((it) => it.key == group.key)
							) {
								matchingModules.push(group);
							}
						});
					});
					this.modules = matchingModules;
					localStorage.setItem('currencies', JSON.stringify(res.currencies.data));
				},
				error: () => {
					// TODO: handle error globally
				},
			});
	}
	onClose(): void {
		if (!this.isDesktop) {
			this.closeSidebar.emit();
		}
	}
	permissions(module) {
		return this.permissionService.hasPermission(module, 'View');
	}
	get userModules() {
		console.log();
		return [];
	}
	navigate(link) {
		this.route.navigate(['admin' + link]);
	}
}
