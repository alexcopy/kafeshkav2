import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogClose, MatDialogRef} from "@angular/material/dialog";
import {OrderItem} from "../../shared/order-item.model";
import {FormsModule, NgForm} from "@angular/forms";
import {ItemService} from "../../shared/item.service";
import {NgForOf} from "@angular/common";
import {OrderService} from "../../shared/order.service";
import {OrderComponent} from "../order.component";

@Component({
  selector: 'app-order-items',
  standalone: true,
  imports: [
    FormsModule,
    NgForOf,
    MatDialogClose
  ],
  templateUrl: './order-items.component.html',
  styles: ``
})
export class OrderItemsComponent implements OnInit {
  formData: OrderItem = new OrderItem();
  isValid: boolean = true;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<OrderItemsComponent>,
    protected itemService: ItemService,
    protected orderService: OrderService,
  ) {
  }

  ngOnInit(): void {

    this.itemService.getItemList();
    console.log(this.data);
    if (this.data.orderItemIndex == null) {
      this.formData = {
        OrderItemId: 0,
        ItemName: '',
        OrderId: this.data.OrderId,
        ItemId: 0,
        Price: 0,
        Quantity: 1,
        Total: 0
      }
    } else {
      this.formData = Object.assign({}, this.orderService.orderItems[this.data.orderItemIndex]);
    }
  }

  updatePrice(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    if (!selectElement) {
      this.formData.Price = 0;
      this.formData.ItemName = "";
    } else {
      const selectedIndex = selectElement.selectedIndex;
      this.formData.Price = this.itemService.itemsList[selectedIndex - 1].price;
      this.formData.ItemName = this.itemService.itemsList[selectedIndex - 1].name;
      this.updateTotal();
    }
  }

  updateTotal() {
    this.formData.Total = parseFloat((this.formData.Price * this.formData.Quantity).toFixed(2));
  }

  onSubmit(form: NgForm) {
    if (this.validateForm(form.value)) {
      if (this.data.orderItemIndex == null) {
        this.orderService.orderItems.push(form.value);
      }else {
        this.orderService.orderItems[this.data.orderItemIndex]=form.value;
      }
      this.dialogRef.close();
    }
  }

  validateForm(formData: OrderItem) {
    this.isValid = true;
    if (formData.ItemId == 0)
      this.isValid = false;
    else if (formData.Quantity == 0)
      this.isValid = false;
    return this.isValid;
  }

}

