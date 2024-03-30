import {Component, OnInit} from '@angular/core';
import {OrderService} from "../shared/order.service";
import {FormsModule, NgForm} from "@angular/forms";
import {NgForOf, NgIf} from "@angular/common";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {OrderItemsComponent} from "./order-items/order-items.component";
import {CustomerService} from "../shared/customer.service";


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
  isValid: boolean = true;

  constructor(protected service: OrderService,
              private dialog: MatDialog,
              protected customerService: CustomerService) {
  }

  ngOnInit(): void {
    this.resetForm();
    this.customerService.getClientsList();
  }

  resetForm(form?: NgForm) {
    if (form != null) {
      form.resetForm();
    }
    this.service.formData = {
      OrderItemId: 0,
      OrderNo: Math.floor(100000 * Math.random() * 900000),
      customerId: 0,
      PMethod: "",
      GTotal: 0
    }
    this.service.orderItems = [];
  }

  AddOrEditOrderItem(orderItemIndex: any, OrderId: number) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.minWidth = "50%";
    dialogConfig.data = {orderItemIndex, OrderId}
    this.dialog.open(OrderItemsComponent, dialogConfig).afterClosed().subscribe(res => {
      this.updateGrandTotal();
    });
  }

  onDeleteOrderItem(OrderItemId: number, i: number) {
    this.service.orderItems.splice(i, 1);
    this.updateGrandTotal();
  }

  updateGrandTotal() {
    this.service.formData.GTotal = this.service.orderItems.reduce((prev, curr) => {
      return prev + curr.Total;
    }, 0);
    this.service.formData.GTotal = parseFloat((this.service.formData.GTotal).toFixed(2));
  }

  validateForm() {
    this.isValid = true;
    if (this.service.formData.customerId == 0) {
      this.isValid = false;
    } else if (this.service.orderItems.length == 0) {
      this.isValid = false;
    }
    return this.isValid;
  }

  onSubmit(form: NgForm) {
    if (this.validateForm()){

    }
  }
}
