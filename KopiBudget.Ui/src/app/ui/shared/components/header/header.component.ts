import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {

	@Input() isSidebarOpen = false;
	@Input() isDesktop = false;
	@Output() toggleSidebar = new EventEmitter<void>();
	onToggle(): void {
		this.toggleSidebar.emit();
	}
}
