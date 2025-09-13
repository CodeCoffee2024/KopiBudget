import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs';
import { AccountService } from '../../core/services/account.service';
import { LoadingService } from '../../core/services/loading.service';
import { ModalService } from '../../core/services/modal.service';
import { ToastService } from '../../core/services/toast.service';
import { AccountConstants, AccountDto } from '../../domain/models/account';
import { ToastType } from '../../domain/models/toast';
import { AccountCardComponent } from './account-card/account-card.component';
import { AccountCreateComponent } from './account-create/account-create.component';
import { AccountUpdateComponent } from './account-update/account-update.component';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [CommonModule, AccountCardComponent],
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent implements OnInit {
  constructor(
    private modalService: ModalService,
    private loadingService: LoadingService,
    private accountService: AccountService,
    private toastService: ToastService
  ) {

  }
  accounts: AccountDto[] = [];
  ngOnInit(): void {
    this.loadAccounts();
  }
  async addAccount() {
    const result = await this.modalService.open(AccountCreateComponent);
    if (result) {
      this.loadAccounts();
    }
  }
  loadAccounts() {
    this.loadingService.show();
    this.accountService
      .getUserAccounts()
      .pipe(
        finalize(() => {
          this.loadingService.hide();
        })
      )
      .subscribe({
        next: (result) => {
          this.accounts = result.data;
        },
        error: (error) => {
          this.toastService.error(
            'Error',
            'Something went wrong.'
          );
        },
      });
    }
    get assets() {
      return this.accounts.filter(it => !it.isDebt);
    }
    get debts() {
      return this.accounts.filter(it => it.isDebt);
    }
    async onEdit(account) {
      const result = await this.modalService.open(AccountUpdateComponent, {account : account});
      if (result) {
        this.loadAccounts();
      }
    }
    async onDelete(account) {
      this.toastService
        .confirm(
          ToastType.CONFIRMATION,
          AccountConstants.DELETECONFIRMATION
        )
        .then((it) => {
          if (it) {
            this.accountService
              .remove(account.id)
              .subscribe(() => {
                this.toastService.success("Success", AccountConstants.DELETESUCCESS)
                this.loadAccounts();
              });
          }
        });
    }
  }
