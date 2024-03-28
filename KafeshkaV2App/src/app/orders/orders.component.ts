import {Component, OnInit} from '@angular/core';
import {OrderService} from "../shared/order.service";
import {FormsModule, NgForm} from "@angular/forms";
import {NgForOf, NgIf} from "@angular/common";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {OrderItemsComponent} from "./order-items/order-items.component";


@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [
    FormsModule,
    NgIf,
    NgForOf
  ],
  templateUrl: './orders.component.html',
  styles: ``
})
export class OrdersComponent implements OnInit {
  constructor(protected service: OrderService, private dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.resetForm();
  }

  resetForm(form?: NgForm) {
    if (form != null) {
      form.resetForm();
    }
    this.service.formData = {
      OrderItemId: 0,
      OrderNo: Math.floor(100000 * Math.random() * 900000).toString(),
      CustomerId: 0,
      PMethod: "",
      GTotal: 0
    }
    this.service.orderItems = [];
  }

  AddOrEditOrderItem(orderItemIndex: number, OrderId: number) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.minWidth="50%";
    dialogConfig.data = {orderItemIndex, OrderId}
    this.dialog.open(OrderItemsComponent);
  }
}

