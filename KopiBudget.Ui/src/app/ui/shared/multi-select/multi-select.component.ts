import { CommonModule } from '@angular/common';
import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  Output,
  ViewChild
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-multi-select',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './multi-select.component.html',
  styleUrls: ['./multi-select.component.scss']
})
export class MultiSelectComponent {
  @Input() label = 'Search';
  @Input() placeholder = 'Type to search...';
  @Input() isLoading = false;
  @Input() field: string;
  @Input() hasSearchIcon = false;
  @Input() searchOptions= []; // Options must be passed from parent
  @Output() searchChanged = new EventEmitter<{ search: string; page: number, clear: boolean }>(); // Emits search event
  @Output() selectedItemsChange = new EventEmitter<[]>(); // Emits selected items

  @ViewChild('dropdownMenu', { static: false }) dropdownMenu!: ElementRef;
  @ViewChild('searchInput', { static: false }) searchInput!: ElementRef;

  searchQuery = '';
  @Input() selectedItems;
  @Input() hasMore = false;
  clear = false;
  page = 1;
  focused = false;
  searchSubject = new Subject<string>();

  constructor() {
    this.selectedItems = this.selectedItems ?? [];
    this.searchSubject.pipe(debounceTime(300)).subscribe(query => {
      this.page = 1;
      this.clear = true;
      this.emitSearch();
    });
  }
  isFocused(focus) {
    this.focused = focus;
  }

  onScroll(): void {
    if (!this.dropdownMenu) return;

    const { scrollTop, scrollHeight, clientHeight } = this.dropdownMenu.nativeElement;
    if (scrollTop + clientHeight >= scrollHeight - 10) {
      this.loadMore();
    }
  }

  onSearch(): void {
    this.isLoading = true;
    this.searchSubject.next(this.searchQuery);
  }

  emitSearch(): void {
    this.searchChanged.emit({ search: this.searchQuery, page: this.page, clear: this.clear });
  }

  loadMore(): void {
    if (!this.hasMore || this.isLoading) return;
    this.clear = false;
    this.page++;
    this.emitSearch();
  }

  selectItem(item): void {
    if (!this.selectedItems.includes(item)) {
      console.log(this.selectedItems);
      console.log(item)
      this.selectedItems.push(item);
      this.selectedItemsChange.emit(this.selectedItems);
    }
    this.searchQuery = '';
  }

  removeItem(item): void {
    this.selectedItems = this.selectedItems.filter(i => i[this.field] !== item[this.field]);
    this.selectedItemsChange.emit(this.selectedItems);
  }
}
