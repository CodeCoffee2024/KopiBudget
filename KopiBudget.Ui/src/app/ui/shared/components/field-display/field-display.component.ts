import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { NullablePipe } from '../../pipes/nullable.pipe';

@Component({
  selector: 'app-field-display',
  standalone: true,
  imports: [CommonModule, NullablePipe],
  templateUrl: './field-display.component.html',
  styleUrls: ['./field-display.component.scss'],
})
export class FieldDisplayComponent {
  @Input() label: string;
  @Input() value: string;
  @Input() nullable: boolean = true;
}
