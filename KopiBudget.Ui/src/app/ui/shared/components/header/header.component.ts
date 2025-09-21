import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../../core/auth/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  constructor(
    private authService: AuthService,
    private route: Router,
  ) {}
  @Input() isSidebarOpen = false;
  @Input() isDesktop = false;
  @Output() toggleSidebar = new EventEmitter<void>();
  onToggle(): void {
    this.toggleSidebar.emit();
  }
  logout() {
    this.authService.logout();
    this.route.navigate(['../../login']);
  }
}
