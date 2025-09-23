import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
	selector: 'app-diff-badge',
	standalone: true,
	imports: [CommonModule],
	templateUrl: './diff-badge.component.html',
	styleUrls: ['./diff-badge.component.scss'],
})
export class DiffBadgeComponent {
	@Input() current: number = 0;
	@Input() previous: number = 0;
	@Input() text: string = '';
	get difference(): number {
		return this.current - this.previous;
	}

	get percentage(): number {
		if (this.previous === 0) return 0;
		return (this.difference / this.previous) * 100;
	}

	get isIncrease(): boolean {
		return this.difference > 0;
	}

	get badgeClass(): string {
		return this.isIncrease ? 'text-success' : 'text-danger';
	}

	get icon(): string {
		return this.isIncrease
			? 'text-success bi-graph-up-arrow'
			: 'text-danger bi-graph-down-arrow';
	}
}
