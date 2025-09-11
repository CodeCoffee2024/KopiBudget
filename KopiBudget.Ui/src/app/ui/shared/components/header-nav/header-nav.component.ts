import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { HeaderNav, NavItem } from '../../../../domain/models/config';

@Component({
  selector: 'app-header-nav',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header-nav.component.html',
  styleUrls: ['./header-nav.component.scss']
})
export class HeaderNavComponent {
	@Input() navs: NavItem[] = [];
	HeaderNav = HeaderNav;
	constructor(private router: Router) {}
	currentNav(nav: string) {
		const navigation = HeaderNav.find(
			(it) => it.page == nav
		);
		return navigation ?? null;
	}
	goTo(link) {
		return 'admin' + link;
	}
}
