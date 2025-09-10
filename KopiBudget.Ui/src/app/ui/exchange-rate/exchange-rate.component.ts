import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ModalService } from '../../core/services/modal.service';
import { ExchangeRateDto } from '../../domain/models/exchange-rate';
import { ExchangeRateUpdateComponent } from './exchange-rate-update/exchange-rate-update.component';

@Component({
  selector: 'app-exchange-rate',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './exchange-rate.component.html',
  styleUrls: ['./exchange-rate.component.scss']
})
export class ExchangeRateComponent {
  @Input() exchangeRates: ExchangeRateDto;
  @Output() reload: EventEmitter<void> = new EventEmitter<void>();
  visibleCount = 5;
  constructor(private modalService: ModalService) {

  }
  get baseCurrency() {
    return this.exchangeRates.baseCurrency;
  }
  get rates() {
    return this.exchangeRates.conversionRates;
  }

  getChangeClass(change?: number): string {
    if (change === undefined || change === null) return '';
    return change > 0 ? 'text-success' : change < 0 ? 'text-danger' : '';
  }

  async changeCurrency(){

    const result = await this.modalService.open(ExchangeRateUpdateComponent);
    if (result) {
      this.exchangeRates.baseCurrency = result;
      this.reload.emit();
    }
  }

  showMore() {
    this.visibleCount = Math.min(this.visibleCount + 5, this.rates.length);
  }
}
