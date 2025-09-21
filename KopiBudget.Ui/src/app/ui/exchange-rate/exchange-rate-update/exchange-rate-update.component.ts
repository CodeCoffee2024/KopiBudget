import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { finalize } from 'rxjs';
import { FormErrorService } from '../../../core/services/form-error.service';
import { LoadingService } from '../../../core/services/loading.service';
import { SystemService } from '../../../core/services/system.service';
import { ToastService } from '../../../core/services/toast.service';
import { CurrencyDto } from '../../../domain/models/currency';
import { InputTypes } from '../../../domain/models/input-type';
import { GenericDropdownListingOption } from '../../../domain/models/listing-option';
import { SystemSetting } from '../../../domain/models/system-setting';
import { InputComponent } from '../../shared/components/input/input.component';
import { SoloSelectComponent } from '../../shared/components/solo-select/solo-select.component';

@Component({
  selector: 'app-exchange-rate-update',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, InputComponent, SoloSelectComponent],
  templateUrl: './exchange-rate-update.component.html',
  styleUrls: ['./exchange-rate-update.component.scss'],
})
export class ExchangeRateUpdateComponent {
  SystemSetting: SystemSetting = new SystemSetting();
  InputTypes = InputTypes;
  isDropdownLoading = false;
  currencies: CurrencyDto[];
  hasMore = false;
  dropdownListingOption: GenericDropdownListingOption = new GenericDropdownListingOption();
  constructor(
    private loadingService: LoadingService,
    private toastService: ToastService,
    private formErrorService: FormErrorService,
    private systemSettingsService: SystemService,
    private activeModal: NgbActiveModal,
  ) {}
  onSubmit() {
    this.systemSettingsService
      .updateCurrency(this.SystemSetting.toSubmitFormCurrency)
      .pipe(
        finalize(() => {
          this.loadingService.hide();
        }),
      )
      .subscribe({
        next: (result) => {
          this.activeModal.close(this.SystemSetting.formCurrency.get('currency').value);
        },
        error: (error) => {
          this.toastService.error('Error', 'Something went wrong.');
          this.formErrorService.setServerErrors(this.SystemSetting.formCurrency, [
            error?.error?.error,
          ]);
        },
      });
  }
  close() {
    this.activeModal.close(null);
  }
  async onSearchChanged({
    search,
    page,
    clear = false,
  }: {
    search: string;
    page: number;
    clear: boolean;
  }) {
    this.isDropdownLoading = true;
    this.dropdownListingOption.search = search;
    this.dropdownListingOption.pageNumber = page;

    if (clear) {
      this.currencies = [];
    }
    this.systemSettingsService.dropdownCurrency(this.dropdownListingOption).subscribe((it) => {
      this.hasMore = page * it.totalCount < it.totalPages * it.totalCount;
      const list = clear ? it.data : [...this.currencies, ...it.data];
      this.currencies = list;
      this.isDropdownLoading = false;
    });
  }

  onSelectionChange(selected): void {
    this.SystemSetting.formCurrency.get('currency').setValue(selected.item);
    // this.refresh();
  }
}
