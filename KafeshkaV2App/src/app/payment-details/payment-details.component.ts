import {Component, OnInit} from '@angular/core';
import {PaymentDetailsFormComponent} from "./payment-details-form/payment-details-form.component";
import {PaymentDetailService} from "../shared/payment-detail.service";
import {NgForOf} from "@angular/common";
import {PaymentDetail} from "../shared/payment-detail.model";
import {ToastrService} from "ngx-toastr";

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
  constructor(public service: PaymentDetailService, private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(pd: PaymentDetail) {
    this.service.formData = Object.assign({}, pd);
  }

  onDelete(paymentDetailId: number) {
    if (confirm("Are you sure you really need to delete the record ?"))
      this.service.deletePaymentDetail(paymentDetailId);
    this.service.deletePaymentDetail(paymentDetailId)
      .subscribe({
        next: res => {
          this.service.list = res as PaymentDetail[];
          this.toastr.error("Deleted Successfully!", "Payment Detail Register")
        },
        error: err => {
          console.log(err);
        }
      });
  }
}
