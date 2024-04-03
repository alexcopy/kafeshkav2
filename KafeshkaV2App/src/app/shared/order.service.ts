import {Injectable} from '@angular/core';
import {Order} from "./order.model";
import {OrderItem} from "./order-item.model";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Customer} from "./customer.model";

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  formData: { orderNo: number; orderId: number; pMethod: string; customerId: number; gTotal: number } = new Order();
  orderItems: OrderItem[] = [];
  ordersList: Order[]=[];
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
}
