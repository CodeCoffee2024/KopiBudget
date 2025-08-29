export enum ToastType {
	CONFIRMATION,
}

export class ToastTypeTitle {
	static readonly titles = {
		[ToastType.CONFIRMATION]: 'Confirmation',
	};
}
