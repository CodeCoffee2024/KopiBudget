import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TitleService {
  constructor(private title: Title) {}

  setTitle(pageTitle: string): void {
    this.title.setTitle(environment.name + ' - ' + pageTitle);
  }

  getTitle(): string {
    return this.title.getTitle();
  }
}
