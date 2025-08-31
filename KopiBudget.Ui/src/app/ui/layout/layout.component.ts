import { CommonModule } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from '../shared/components/header/header.component';
import { SidebarComponent } from '../shared/components/sidebar/sidebar.component';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [CommonModule, HeaderComponent, SidebarComponent, RouterOutlet],
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
	isSidebarOpen = false;
	isDesktop = false;

	constructor(private router: Router) {}

	ngOnInit() {
		this.checkScreenSize();
	}

	@HostListener('window:resize', ['$event'])
	onResize(event: any) {
		this.checkScreenSize();
	}

	checkScreenSize(): void {
		this.isDesktop = window.innerWidth >= 1024;
		if (this.isDesktop) {
			this.isSidebarOpen = true;
		} else {
			this.isSidebarOpen = false;
		}
	}

	toggleSidebar(): void {
		this.isSidebarOpen = !this.isSidebarOpen;
	}

	closeSidebar(): void {
		if (!this.isDesktop) {
			this.isSidebarOpen = false;
		}
	}
}
