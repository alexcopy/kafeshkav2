import {Component, OnInit} from '@angular/core';
import {PaymentDetailsFormComponent} from "./payment-details-form/payment-details-form.component";
import {PaymentDetailService} from "../shared/payment-detail.service";
import {NgForOf} from "@angular/common";
import {PaymentDetail} from "../shared/payment-detail.model";

@Component({
  selector: 'app-payment-details',
  standalone: true,
  imports: [
    PaymentDetailsFormComponent,
    NgForOf
  ],
  templateUrl: './payment-details.component.html',
  styles: ``
})
export class PaymentDetailsComponent implements OnInit {
  constructor(public service: PaymentDetailService) {
  }

  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(pd: PaymentDetail) {
    this.service.formData = Object.assign({}, pd);
  }
}
