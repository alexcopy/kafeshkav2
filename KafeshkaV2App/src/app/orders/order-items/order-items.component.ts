import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogClose, MatDialogRef} from "@angular/material/dialog";
import {OrderItem} from "../../shared/order-item.model";
import {FormsModule, NgForm} from "@angular/forms";
import {ItemService} from "../../shared/item.service";
import {NgForOf} from "@angular/common";
import {OrderService} from "../../shared/order.service";

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
    @Inject(MAT_DIALOG_DATA) public data: OrderItem,
    public dialogRef: MatDialogRef<OrderItemsComponent>,
    protected itemService: ItemService,
    protected orderService: OrderService,
  ) {
  }

  ngOnInit(): void {
    this.itemService.getItemList()
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
    if(this.validateForm(form.value)) {
      this.orderService.orderItems.push(form.value);
      this.dialogRef.close();
    }
  }

  validateForm(formData: OrderItem) {
    this.isValid = true;
    if (formData.OrderId == 0)
      this.isValid = false;
    if (formData.Quantity == 0)
      this.isValid = false;
    if (formData.Price == 0)
      this.isValid = false;
    return this.isValid;
  }

}

