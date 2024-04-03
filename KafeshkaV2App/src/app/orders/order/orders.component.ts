import {Component, OnInit} from '@angular/core';
import {OrderService} from "../../shared/order.service";
import {NgForOf} from "@angular/common";
import {Router, RouterLink} from "@angular/router";


@Component({
  selector: 'app-order',
  standalone: true,
  imports: [
    NgForOf,
    RouterLink
  ],
  templateUrl: './orders.component.html',
  styles: ``
})
export class OrdersComponent implements OnInit {

  constructor(protected service: OrderService, private router: Router) {
  }

  ngOnInit(): void {
    this.service.getOrdersList();
  }

  openForEdit(orderId: number) {
    this.router.navigate(['orders/edit/' + orderId]);
  }
}
