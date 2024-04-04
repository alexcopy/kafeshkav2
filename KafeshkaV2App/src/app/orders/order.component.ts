import {Component, OnInit} from '@angular/core';
import {OrderService} from "../shared/order.service";
import {FormsModule, NgForm} from "@angular/forms";
import {NgForOf, NgIf} from "@angular/common";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {OrderItemsComponent} from "./order-items/order-items.component";
import {CustomerService} from "../shared/customer.service";
import {ToastrService} from "ngx-toastr";
import {ActivatedRoute, Router, RouterLink} from "@angular/router";


@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [
    FormsModule,
    NgIf,
    NgForOf,
    RouterLink
  ],
  templateUrl: './order.component.html',
  styles: ``
})
export class OrderComponent implements OnInit {
  isValid: boolean = true;

  constructor(protected service: OrderService,
              private dialog: MatDialog,
              protected customerService: CustomerService,
              private toastr: ToastrService,
              private router: Router,
              private currentRoute: ActivatedRoute) {
  }

  ngOnInit(): void {
    let orderId = this.currentRoute.snapshot.paramMap.get("id");
    if (orderId == null)
      this.resetForm();
    else {
      this.service.getOrderById(parseInt(orderId));
    }
    this.customerService.getClientsList();
  }

  resetForm(form?: NgForm) {
    if (form != null) {
      form.resetForm();
    }
    this.service.formData = {
      orderId: 0,
      orderNo: Math.floor(100000 * Math.random() * 900000),
      customerId: 0,
      pMethod: "",
      gTotal: 0,
      deletedOrderItemIds: ""
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
    if (OrderItemId != null) {
      this.service.formData.deletedOrderItemIds += OrderItemId + ",";
    }
    this.service.orderItems.splice(i, 1);
    this.updateGrandTotal();
  }

  updateGrandTotal() {
    this.service.formData.gTotal = this.service.orderItems.reduce((prev, curr) => {
      return prev + curr.total;
    }, 0);
    this.service.formData.gTotal = parseFloat((this.service.formData.gTotal).toFixed(2));
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
    if (this.validateForm()) {
      this.service.saveOrUpdateOrder().subscribe(res => {
        this.resetForm();
        this.toastr.success("Your order Submitted Successfully!", "Kafeshka App");
        this.router.navigate(["/orders"]);
      });
    }
  }
}

