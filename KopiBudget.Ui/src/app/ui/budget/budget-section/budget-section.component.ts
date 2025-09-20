import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { BudgetDto } from '../../../domain/models/budget';
import { ExchangeRateDto } from '../../../domain/models/exchange-rate';

@Component({
  selector: 'app-budget-section',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './budget-section.component.html',
  styleUrls: ['./budget-section.component.scss']
})
export class BudgetSectionComponent implements OnInit{
  @Input() exchangeRate: ExchangeRateDto;
  @Input() budgets: BudgetDto[]= [];
  selectedBudget: BudgetDto; // default
  constructor() {
    console.log(this.budgets)
  }
  ngOnInit(): void {
    this.selectedBudget = this.budgets[0]; // default
    console.log(this.budgets)
  }
  onSelectBudget(budget: any) {
    this.selectedBudget = budget;
  }
}
