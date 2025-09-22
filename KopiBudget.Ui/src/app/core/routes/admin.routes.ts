import { Routes } from '@angular/router';
import { AccountComponent } from '../../ui/account/account.component';
import { BudgetComponent } from '../../ui/budget/budget.component';
import { LayoutComponent } from '../../ui/layout/layout.component';
import { TransactionComponent } from '../../ui/transaction/transaction.component';
import { UserComponent } from '../../ui/user/user.component';
import { AuthGuard } from '../auth/auth.guard';
import {
	AccountPermissions,
	BudgetPermissions,
	TransactionPermissions,
	UserPermissions,
} from '../constants/permission';

export const adminRoutes: Routes = [
	{
		path: '',
		component: LayoutComponent,
		children: [
			{
				path: 'accounts',
				component: AccountComponent,
				canActivate: [AuthGuard],
				data: {
					permission: AccountPermissions.View + ',',
				},
			},
			{
				path: 'transactions',
				component: TransactionComponent,
				canActivate: [AuthGuard],
				data: {
					permission: TransactionPermissions.View + ',',
				},
			},
			{
				path: 'budgets',
				component: BudgetComponent,
				canActivate: [AuthGuard],
				data: {
					permission: BudgetPermissions.View + ',',
				},
			},
			{
				path: 'users',
				component: UserComponent,
				canActivate: [AuthGuard],
				data: {
					permission: UserPermissions.View + ',',
				},
			},
		],
	},
];
