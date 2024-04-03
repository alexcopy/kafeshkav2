import {Component, OnInit} from '@angular/core';
import {OrderService} from "../../shared/order.service";
import {NgForOf} from "@angular/common";


@Component({
  selector: 'app-order',
  standalone: true,
  imports: [
    NgForOf
  ],
  templateUrl: './orders.component.html',
  styles: ``
})
export class OrdersComponent implements OnInit{

  constructor(protected service: OrderService) {
  }
  ngOnInit(): void {
    this.service.getOrdersList();
  }
}
