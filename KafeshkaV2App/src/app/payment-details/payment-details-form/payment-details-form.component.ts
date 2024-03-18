import {Component} from '@angular/core';
import {PaymentDetailService} from "../../shared/payment-detail.service";
import {FormsModule, NgForm} from "@angular/forms";
import {PaymentDetail} from "../../shared/payment-detail.model";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-payment-details-form',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './payment-details-form.component.html',
  styles: ``
})
export class PaymentDetailsFormComponent {
  constructor(public service: PaymentDetailService, private toastr: ToastrService) {
  }

  onSubmit(form: NgForm) {
    this.service.formSubmitted = true;
    if (form.valid) {
      this.service.postPaymentDetail()
        .subscribe({
          next: res => {
            this.service.list = res as PaymentDetail[];
            this.service.resetForm(form);
            this.toastr.success("Inserted Successfully!", "Payment Detail Register")
          },
          error: err => {
            console.log(err);
          }
        });
    }

  }
}
