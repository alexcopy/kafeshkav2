import {Component, OnInit} from '@angular/core';
import {OrderService} from "../shared/order.service";
import {FormsModule, NgForm} from "@angular/forms";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [
    FormsModule,
    NgIf
  ],
  templateUrl: './orders.component.html',
  styles: ``
})
export class OrdersComponent implements OnInit {
  constructor(protected service: OrderService) {
  }

  ngOnInit(): void {
    this.resetForm();
  }

  resetForm(form?: NgForm) {
    if (form!=null) {
      form.resetForm();
    }
    this.service.formData = {
      OrderItemId: 0,
      OrderNo: Math.floor(100000*Math.random()*900000).toString(),
      CustomerId: 0,
      PMethod: "",
      GTotal: 0
    }
  }
}

