import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { LoadingService } from '../../../../core/services/loading.service';
import { SystemService } from '../../../../core/services/system.service';
import { ModuleGroupResponse } from '../../../../domain/models/module';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
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
		private route: Router
	) {}

	ngOnInit(): void {
		this.systemService
			.getModuleGroups()
			.pipe(
				finalize(() => {
					this.loadingService.hide();
					this.isLoading = false;
				})
			)
			.subscribe({
				next: (res) => {
					this.modules = res.data;
					console.log(res);
				},
				error: () => {},
			});
	}
	onClose(): void {
		if (!this.isDesktop) {
			this.closeSidebar.emit();
		}
	}
	navigate(link) {
		this.route.navigate(['admin' + link]);
	}
}
