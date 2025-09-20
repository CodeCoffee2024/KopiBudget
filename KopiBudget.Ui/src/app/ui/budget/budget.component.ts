import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { finalize, forkJoin } from 'rxjs';
import { BudgetService } from '../../core/services/budget.service';
import { ExchangeRateService } from '../../core/services/exchange-rate.service';
import { LoadingService } from '../../core/services/loading.service';
import { ModalService } from '../../core/services/modal.service';
import { PersonalCategoryService } from '../../core/services/personal-category.service';
import { ToastService } from '../../core/services/toast.service';
import { BudgetConstants, BudgetDto } from '../../domain/models/budget';
import { ExchangeRateDto } from '../../domain/models/exchange-rate';
import { PersonalCategoryDto } from '../../domain/models/personal-category';
import { BudgetCreateComponent } from './budget-create/budget-create.component';
import { BudgetLimitSectionComponent } from './budget-limit-section/budget-limit-section.component';
import { BudgetSectionComponent } from './budget-section/budget-section.component';

@Component({
  selector: 'app-budget',
  standalone: true,
  imports: [CommonModule, BudgetCreateComponent, BudgetSectionComponent, BudgetLimitSectionComponent],
  templateUrl: './budget.component.html',
  styleUrls: ['./budget.component.scss']
})
export class BudgetComponent implements OnInit {
  isLoading = true;
  personalCategories: PersonalCategoryDto[] = [];
  budgets: BudgetDto[] = [];
  exchangeRate: ExchangeRateDto;
  constructor(
    private modalService: ModalService,
    private loadingService: LoadingService,
    private budgetService: BudgetService,
    private exchangeRateService: ExchangeRateService,
    private personalCategoryService: PersonalCategoryService,
    private toastService: ToastService
  ) {

  }
  ngOnInit(): void {
    this.loadingService.show();
    forkJoin({
      personalCategories: this.personalCategoryService.getAll(),
      rates: this.exchangeRateService.getAll(),
      budgets: this.budgetService.getAll()
    })
    .pipe(
      finalize(() => {
        this.isLoading = false;
        this.loadingService.hide();
      })
    )
    .subscribe({
      next: (res) => {
        this.budgets = res.budgets.data;
        this.exchangeRate = res.rates.data;
        this.personalCategories = res.personalCategories.data;

      },
      error: () => {
      }
    });
  }
  async addBudget() {
    const result = await this.modalService.open(BudgetCreateComponent, {personalCategories: this.personalCategories});
    if (result) {
      this.toastService.success("Success", BudgetConstants.CREATESUCCESS)
      // this.loadAccounts();
    }
  }
  onEdit(category){

  }
  onDelete(category){

  }
}
