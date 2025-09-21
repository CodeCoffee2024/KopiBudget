import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-account-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './account-card.component.html',
  styleUrls: ['./account-card.component.scss'],
})
export class AccountCardComponent implements OnInit {
  @Input() name: string = 'John Doe';
  @Input() balance: number = 0;
  @Output() deleteEvent: EventEmitter<void> = new EventEmitter<void>();
  @Output() editEvent: EventEmitter<void> = new EventEmitter<void>();

  ngOnInit(): void {
    // Pick a random gradient for this card
  }
  onEdit() {
    this.editEvent.emit();
  }
  onDelete() {
    this.deleteEvent.emit();
  }
}
