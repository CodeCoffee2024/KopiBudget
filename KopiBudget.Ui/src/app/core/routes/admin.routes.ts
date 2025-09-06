import { Routes } from '@angular/router';
import { AccountComponent } from '../../ui/account/account.component';
import { LayoutComponent } from '../../ui/layout/layout.component';

export const adminRoutes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        children: [
            { path: 'accounts', component: AccountComponent }
        ]
    }
];
