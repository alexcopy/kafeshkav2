import {Injectable} from '@angular/core';
import {Order} from "./order.model";
import {OrderItem} from "./order-item.model";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Customer} from "./customer.model";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  formData: Order = {
    orderNo: 0,
    orderId: 0,
    pMethod: '',
    customer: "",
    customerId: 0,
    gTotal: 0,
    deletedOrderItemIds: ''
  };
  orderItems: OrderItem[] = [];
  ordersList: Order[] = [];
  url = environment.apiBaseUrl + "/Order";

  constructor(private http: HttpClient) {
  }

  saveOrUpdateOrder() {
    var body = {
      ...this.formData,
      OrderItems: this.orderItems
    };
    return this.http.post(this.url, body);
  }


  deleteOrder(orderId: number): Observable<void> {
    const deleteUrl = `${this.url}/${orderId}`;
    return this.http.delete<void>(deleteUrl);
  }


  getOrdersList() {
    this.http.get(this.url).subscribe({
      next: res => {
        this.ordersList = res as Order[];
      },
      error: err => {
        console.log(err);
      }
    });
  }

  getOrderById(id: number) {
    this.http.get<{ order: Order, orderItems: OrderItem[] }>(this.url + "/" + id).subscribe({
      next: (res: { order: Order, orderItems: OrderItem[] }) => {
        this.formData = res.order;
        this.orderItems = res.orderItems;
      },
      error: err => {
        console.log(err);
      }
    });
  }
}
