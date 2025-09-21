import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Injectable({
  providedIn: 'root',
})
export class ModalService {
  constructor(private modalService: NgbModal) {}

  open(component, data?) {
    const modalRef = this.modalService.open(component, {
      backdrop: 'static',
      keyboard: false,
      centered: true,
    });
    if (data) {
      Object.assign(modalRef.componentInstance, data);
    }
    return modalRef.result; // Returns a Promise
  }
}
