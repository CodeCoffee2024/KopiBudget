import { CommonModule } from '@angular/common';
import { AfterViewChecked, Component, ElementRef, NgZone, OnInit, ViewChild } from '@angular/core';
import { Carousel } from 'bootstrap';
import { finalize, forkJoin, interval, Subscription } from 'rxjs';
import { AccountService } from '../../core/services/account.service';
import { ExchangeRateService } from '../../core/services/exchange-rate.service';
import { LoadingService } from '../../core/services/loading.service';
import { ModalService } from '../../core/services/modal.service';
import { ToastService } from '../../core/services/toast.service';
import { TransactionService } from '../../core/services/transaction.service';
import { AccountDto } from '../../domain/models/account';
import { ExchangeRateDto } from '../../domain/models/exchange-rate';
import { TransactionDto, TransactionListingOption } from '../../domain/models/transaction';
import { AccountCardComponent } from '../account/account-card/account-card.component';
import { ExchangeRateComponent } from '../exchange-rate/exchange-rate.component';
import { ListingPaginationComponent } from '../shared/listing-pagination/listing-pagination.component';
import { ThHeaderComponent } from '../shared/th-header/th-header.component';
import { TransactionCreateComponent } from './transaction-create/transaction-create.component';
import { TransactionSearchComponent } from './transaction-search/transaction-search.component';

@Component({
  selector: 'app-transaction',
  standalone: true,
  imports: [CommonModule, ExchangeRateComponent, AccountCardComponent, TransactionCreateComponent, TransactionSearchComponent, ThHeaderComponent, ListingPaginationComponent],
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.scss']
})
export class TransactionComponent implements OnInit, AfterViewChecked {
  exchangeRates?: ExchangeRateDto;
  @ViewChild('accountCarousel', { static: false }) accountCarousel?: ElementRef<HTMLDivElement>;
  private carouselInstance?: Carousel;
  private initialized = false;
  private slidHandler?: EventListenerOrEventListenerObject;
  results: TransactionDto[] = [];
  error?: string;
  isLoading = false;
  accounts: AccountDto[] = [];
  private sub?: Subscription;
	listingData;
  listingOption: TransactionListingOption = new TransactionListingOption();

  constructor(
    private exchangeRateService: ExchangeRateService,
    private accountService: AccountService,
    private loadingService: LoadingService,
    private transactionService: TransactionService,
    private modalService: ModalService,
    private toastService: ToastService,
    private ngZone: NgZone

  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.loadRates();
    this.sub = interval(12000).subscribe(() => this.loadRates());
    this.loadTransactions();
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
    this.disposeCarousel();
  }
  loadTransactions() {
    this.loadingService.show();
    this.transactionService.getTransactions(this.listingOption)
    .pipe(
      finalize(() => {
        this.loadingService.hide();
      })
    )
    .subscribe({
      next: (result) => {
        this.results = result.data;
				this.listingData = result;
      },
      error: (error) => {
        this.toastService.error(
          'Error',
          'Something went wrong.'
        );
      },
    });
  }
  loadRates(): void {

    forkJoin({
      accounts: this.accountService.getUserAccounts(),
      rates: this.exchangeRateService.getAll()
    })
    .pipe(
      finalize(() => {
        this.isLoading = false;
      })
    )
    .subscribe({
      next: (res) => {
        this.exchangeRates = res.rates.data;
        this.accounts = res.accounts.data;

      },
      error: () => {
        this.error = 'Failed to load rates';
      }
    });
  }
  reload(item) {
    this.loadRates();
  }
  currentIndex = 0;
  ngAfterViewChecked(): void {
      if (!this.isLoading && this.accountCarousel && this.accounts?.length && !this.initialized) {
        setTimeout(() => this.initCarousel(), 0);
      }
    }

  private initCarousel(): void {
    if (!this.accountCarousel) return;

    const el = this.accountCarousel.nativeElement;

    this.disposeCarousel();

    this.carouselInstance = new Carousel(el, { interval: false });

    this.slidHandler = (event: any) => {
      this.ngZone.run(() => {
        this.currentIndex = event?.to ?? 0;
      });
    };

    el.addEventListener('slid.bs.carousel', this.slidHandler);

    const items = Array.from(el.querySelectorAll('.carousel-item'));
    const activeIndex = items.findIndex(i => i.classList.contains('active'));
    this.currentIndex = activeIndex >= 0 ? activeIndex : 0;

    this.initialized = true;
  }

  prev(): void {
    if (!this.carouselInstance) return;
    this.carouselInstance.prev();
  }

  next(): void {
    if (!this.carouselInstance) return;
    this.carouselInstance.next();
  }

  private disposeCarousel(): void {
    if (this.carouselInstance) {
      try {
        this.carouselInstance.dispose();
      } catch { /* ignore */ }
      this.carouselInstance = undefined;
    }
    if (this.accountCarousel && this.slidHandler) {
      try {
        this.accountCarousel.nativeElement.removeEventListener('slid.bs.carousel', this.slidHandler);
      } catch { /* ignore */ }
      this.slidHandler = undefined;
    }
    this.initialized = false;
  }
  async addTransaction(){
    const result = await this.modalService.open(TransactionCreateComponent);
    if (result) {
      this.loadTransactions();
    }
  }
  sortEvent(result) {
    this.listingOption.orderBy = result.sortDirection=="desc" ? result.name+"-" : result.name;
    this.loadTransactions();
  }

  refreshList(listingOption) {
    this.listingOption = listingOption;
    this.listingOption.categoryIds = listingOption.categoryIds?.split(',');
    this.listingOption.accountIds = listingOption.accountIds?.split(',');
    this.loadTransactions();
  }
  onPageChange(page) {
    this.listingOption.pageNumber = page;
    this.loadTransactions();
  }
}
