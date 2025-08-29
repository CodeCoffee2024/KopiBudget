import { Injectable } from '@angular/core';
import { BehaviorSubject, delay } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
	private loadingSubject = new BehaviorSubject<boolean>(
		false
	);
	public loading$ = this.loadingSubject
		.asObservable()
		.pipe(delay(0));

	show() {
		this.loadingSubject.next(true);
	}

	hide() {
		this.loadingSubject.next(false);
	}
	isLoading(): boolean {
		return this.loadingSubject.value;
	}
}
