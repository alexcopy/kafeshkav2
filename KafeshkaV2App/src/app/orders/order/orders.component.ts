import {Component, OnInit} from '@angular/core';
import {OrderService} from "../../shared/order.service";
import {NgForOf} from "@angular/common";
import {Router, RouterLink} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {catchError, EMPTY, tap} from "rxjs";


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

  constructor(protected service: OrderService, private router: Router, private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.refreshList();
  }

  refreshList() {
    this.service.getOrdersList();
  }

  openForEdit(orderId: number) {
    this.router.navigate(['order/edit/' + orderId]);
  }


  onOrderDelete(orderId: number): void {
    if (confirm("Are you sure you want to delete the order?")) {
      this.service.deleteOrder(orderId)
        .pipe(
          tap(() => {
            this.toastr.success('Order deleted successfully', 'KafeShka App');
            this.refreshList();
          }),
          catchError(error => {
            this.toastr.error('Failed to delete order', 'KafeShka App');
            return EMPTY;
          })
        ).subscribe();
    }
  }
}
