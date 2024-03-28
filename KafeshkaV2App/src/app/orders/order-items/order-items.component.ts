import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {OrderItem} from "../../shared/order-item.model";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-order-items',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './order-items.component.html',
  styles: ``
})
export class OrderItemsComponent implements OnInit {
  formData: OrderItem = new OrderItem();

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: OrderItem,
    public dialogRef: MatDialogRef<OrderItemsComponent>
  ) {
  }
  ngOnInit(): void {

  }
}
