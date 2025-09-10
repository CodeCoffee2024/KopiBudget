import { CommonModule } from '@angular/common';
import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl, FormsModule } from '@angular/forms';
import { debounceTime, Subject } from 'rxjs';

@Component({
	selector: 'app-solo-select',
	standalone: true,
	imports: [CommonModule, FormsModule],
	templateUrl: './solo-select.component.html',
	styleUrls: ['./solo-select.component.scss']
})
export class SoloSelectComponent
	implements OnInit
{
	@Input() label = 'Search';
	@Input() placeholder = 'Type to search...';
	@Input() isLoading = false;
	@Input() field: string;
	@Input() subFields: string[] = [];
	@Input() subFieldLabels: string[] = [];
	@Input() hideLabel = false;
	@Input() hasSearchIcon = false;
	@Input() errorName = '';
	@Input() controlName;
	@Input() searchOptions = []; // Options must be passed from parent
	@Output() addSearchResult = new EventEmitter<string>();
	@Output() searchChanged = new EventEmitter<{
		search: string;
		page: number;
		clear: boolean;
	}>(); // Emits search event
	@Output() selectedItemChange = new EventEmitter<object>(); // Emits selected items

	@ViewChild('dropdownMenu', { static: false })
	dropdownMenu!: ElementRef;
	@ViewChild('searchInput', { static: false })
	searchInput!: ElementRef;

	searchQuery = '';
	@Input() selectedItem;
	@Input() formGroup;
	@Input() hasMore = false;
	@Input() addNoResult = true;
	clear = false;
	page = 1;
	focused = false;
	searchSubject = new Subject<string>();
	ngOnInit(): void {
		this.selectedItem = this.selectedItem ?? null;
		this.searchSubject
			.pipe(debounceTime(300))
			.subscribe(() => {
				this.page = 1; // Reset page
				this.clear = true;
				this.emitSearch();
			});
	}
	get formControl(): FormControl {
		return this.formGroup?.get(
			this.controlName
		) as FormControl;
	}
	isFocused(focus) {
		this.focused = focus;
	}

	onScroll(): void {
		if (!this.dropdownMenu) return;

		const { scrollTop, scrollHeight, clientHeight } =
			this.dropdownMenu.nativeElement;
		if (scrollTop + clientHeight >= scrollHeight - 10) {
			this.loadMore();
		}
	}

	onSearch(): void {
		this.isLoading = true;
		this.searchSubject.next(this.searchQuery);
	}

	emitSearch(): void {
		this.searchChanged.emit({
			search: this.searchQuery,
			page: this.page,
			clear: this.clear,
		});
	}

	loadMore(): void {
		if (!this.hasMore || this.isLoading) return;
		this.clear = false;
		this.page++;
		this.emitSearch();
	}

	/** Selects an item */
	selectItem(item): void {
		this.selectedItemChange.emit({
			item: item,
			formGroup: this.formGroup,
		});
		this.searchQuery = '';
		this.selectedItem = item;
	}

	/** Removes a selected item */
	clearSelection(): void {
		this.selectedItem = null;
		this.selectedItemChange.emit({
			item: null,
			formGroup: this.formGroup,
		});
	}
	get errors(): string[] {
		if (!this.formControl) return [];
		const controlErrors = this.formControl.errors || {};

		const errorMessages: string[] = [];
		Object.keys(controlErrors).forEach((errorKey) => {
			if (errorKey === 'required') {
				errorMessages.push(`${this.label} is required`);
			} else if (errorKey === 'serverError') {
				errorMessages.push(controlErrors['serverError']);
			} else {
				errorMessages.push(
					`${this.label} ${errorKey
						.replace(/([A-Z])/g, ' $1')
						.toLowerCase()}`
				);
			}
		});

		const serverErrors = this.formGroup.errors || {};
		Object.keys(serverErrors).forEach((errorKey) => {
			if (errorKey === 'serverError') {
				Object.keys(serverErrors['serverError']).forEach(
					(serverKey) => {
						if (
							serverKey.toLocaleLowerCase() ==
							this.errorName.toLocaleLowerCase()
						) {
							this.formControl.markAsTouched();
							this.formControl.setErrors({ invalid: true });
							// errorMessages.push(serverErrors['serverError'][serverKey]);
						}
					}
				);
			}
		});
		return errorMessages;
	}
	get isRequired(): boolean {
		return !!this.formControl?.errors?.['required'];
	}
	addSearch() {
		this.addSearchResult.emit(this.searchQuery);
	}
}
