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
    let detailId = this.service.formData.paymentDetailId;
    if (form.valid) {
      if (detailId == 0) {
        this.insertRecord(form);
      } else {
        this.updateRecord(form)
      }
    }
  }

  insertRecord(form: NgForm) {
    this.service.postPaymentDetail()
      .subscribe({
        next: res => {
          this.service.list = res as PaymentDetail[];
          this.service.resetForm(form);
          this.toastr.success("Inserted Successfully!", "Payment Detail Register")
        },
        error: err => {
          console.log(err);
          const errorMessage = err.error instanceof Array ? err.error.join(', ') : 'An error occurred.';
          // Display error message using Toastr
          this.toastr.error(errorMessage, "Error");
        }
      });
  }

  updateRecord(form: NgForm) {
    this.service.putPaymentDetail()
      .subscribe({
        next: res => {
          this.service.list = res as PaymentDetail[];
          this.service.resetForm(form);
          this.toastr.info("Updated Successfully!", "Payment Detail Register")
        },
        error: err => {
          console.log(err);
          const errorMessage = err.error instanceof Array ? err.error.join('\n\r') : 'An error occurred.';
          // Display error message using Toastr
          this.toastr.error(errorMessage, "Error");
        }
      });
  }
}
