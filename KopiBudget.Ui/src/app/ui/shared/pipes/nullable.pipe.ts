import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
	name: 'nullable',
	standalone: true,
})
export class NullablePipe implements PipeTransform {
	transform(value: unknown): unknown {
		return value === null ||
			value === undefined ||
			value == ''
			? '--'
			: value;
	}
}
