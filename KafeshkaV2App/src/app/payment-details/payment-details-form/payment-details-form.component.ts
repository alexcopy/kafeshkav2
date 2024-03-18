import { Component } from '@angular/core';
import {PaymentDetailService} from "../../shared/payment-detail.service";
import {FormsModule, NgForm} from "@angular/forms";

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
constructor(public service : PaymentDetailService) {
}

  onSubmit(form: NgForm) {
    this.service.postPaymentDetail()
      .subscribe({
        next:res=>{
          console.log(res);
        },
        error:err => {console.log(err); }
      });
  }
}
