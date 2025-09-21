export enum Page {
  Dashboard = 'dashboard',
  Transaction = 'transactions',
  Account = 'accounts',
  User = 'users',
  Budget = 'budgets',
  None = '',
}

export interface NavItem {
  page: Page;
  label: string;
  link: string;
}

export const HeaderNav: NavItem[] = [
  { page: Page.None, label: '', link: '/' },
  {
    page: Page.Dashboard,
    label: 'Dashboard',
    link: '/dashboard',
  },
  {
    page: Page.Transaction,
    label: 'Transactions',
    link: '/transactions',
  },
  { page: Page.Account, label: 'Account', link: '/accounts' },
  { page: Page.Budget, label: 'Budget', link: '/budgets' },
  {
    page: Page.User,
    label: 'Users',
    link: '/users',
  },
];
