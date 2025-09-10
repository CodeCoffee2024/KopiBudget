import { Routes } from '@angular/router';
import { AccountComponent } from '../../ui/account/account.component';
import { LayoutComponent } from '../../ui/layout/layout.component';
import { TransactionComponent } from '../../ui/transaction/transaction.component';

export const adminRoutes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        children: [
            { path: 'accounts', component: AccountComponent },
            { path: 'transactions', component: TransactionComponent }
        ]
    }
];
