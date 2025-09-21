import { Injectable } from '@angular/core';
import Swal, { SweetAlertIcon, SweetAlertResult } from 'sweetalert2';
import { ToastType, ToastTypeTitle } from '../../domain/models/toast';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  private show(icon: SweetAlertIcon, title: string, text?: string): void {
    Swal.fire({
      icon,
      title,
      text: text || '',
      showConfirmButton: false,
      timer: 3000,
      position: 'center',
      customClass: {
        popup: 'custom-alert-box',
      },
      background: '#fff',
      width: '300px',
      timerProgressBar: false,
    });
  }

  success(message: string, subtitle?: string): void {
    this.show('success', message, subtitle);
  }

  error(message: string, subtitle?: string): void {
    this.show('error', message, subtitle);
  }

  info(message: string, subtitle?: string): void {
    this.show('info', message, subtitle);
  }

  warning(message: string, subtitle?: string): void {
    this.show('warning', message, subtitle);
  }

  confirm(
    type: ToastType,
    text?: string,
    confirmButtonText: string = 'Yes',
    cancelButtonText: string = 'No',
  ): Promise<boolean> {
    const title = ToastTypeTitle.titles[type];
    return Swal.fire({
      title,
      text: text || '',
      icon: 'question',
      showCancelButton: true,
      confirmButtonText,
      cancelButtonText,
      focusCancel: true,
      customClass: {
        confirmButton: 'btn btn-success me-2',
        cancelButton: 'btn btn-secondary',
        popup: 'custom-alert-box',
      },
      background: '#fff',
      width: '400px',
    }).then((result: SweetAlertResult) => result.isConfirmed);
  }
}
