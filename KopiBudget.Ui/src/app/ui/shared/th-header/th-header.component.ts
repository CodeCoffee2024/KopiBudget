import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-th-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './th-header.component.html',
  styleUrls: ['./th-header.component.scss']
})
export class ThHeaderComponent {
  @Input() name: string;
  @Input() description: string;
  @Input() sortBy: string;
  @Input() sortDirection: string;
  @Input() alignment: string;
  @Input() isSortable = false;
  @Output() sortResult = new EventEmitter<{name, sortDirection}>();
  sort() {
    if (!this.isSortable) return;
    this.sortDirection = this.sortDirection == 'asc' ? 'desc' : 'asc';
    this.sortResult.emit({name: this.name, sortDirection: this.sortDirection});
  }
  get isAsc(): boolean {
    return this.sortDirection == 'asc';
  }
  get alignmentClass() {
    if (this.alignment == 'right') {
      return ' d-flex justify-content-end';
    }
    return ' d-flex ';
  }
}
