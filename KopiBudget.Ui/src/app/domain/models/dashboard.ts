export interface DashboardBalanceExpenseDto extends DashboardSummaryDto {
	previousMonth: Number;
}

export interface DashboardSummaryDto {
	label: string;
	value: Number;
}
