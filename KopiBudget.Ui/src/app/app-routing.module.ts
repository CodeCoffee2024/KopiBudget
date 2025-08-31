import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/auth/auth.guard';
import { DashboardPermissions } from './core/constants/permissions/dashboard';
import { adminRoutes } from './core/routes/admin.routes';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () =>
      import('./ui/auth/login/login.component').then(m => m.LoginComponent),
  },
  {
    path: 'admin',
    canActivate: [AuthGuard],
    data: {
      permission:
        DashboardPermissions.View + ","
    },
    children: adminRoutes
  },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
