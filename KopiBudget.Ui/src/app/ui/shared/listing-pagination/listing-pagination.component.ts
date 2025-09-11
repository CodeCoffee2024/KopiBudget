import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';

@Component({
  selector: 'app-listing-pagination',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './listing-pagination.component.html',
  styleUrls: ['./listing-pagination.component.scss']
})
export class ListingPaginationComponent implements OnInit {
	@Input() listingData;
	@Output() changePage = new EventEmitter<number>();
	ngOnInit() {}
	onPageChange(page) {
		this.changePage.emit(page);
	}
	get pages(): number[] {
		return Array(this.listingData.totalPages)
			.fill(0)
			.map((_, i) => i + 1);
	}
	get summaryPagination() {
		return (
			'Page ' +
			this.listingData.pageNumber +
			' of ' +
			this.listingData.totalPages
		);
	}
	get summaryResults() {
		return 'Total results: ' + this.listingData.totalCount;
	}
}
