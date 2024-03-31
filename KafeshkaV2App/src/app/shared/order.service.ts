import {Injectable} from '@angular/core';
import {Order} from "./order.model";
import {OrderItem} from "./order-item.model";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  formData: Order = new Order();
  orderItems: OrderItem[] = [];

  constructor(private http: HttpClient) {
  }

  saveOrUpdateOrder() {
    var body = {
      ...this.formData,
      OrderItems: this.orderItems,
      orderNo: '"' + this.formData.orderNo + '"',
    };
    return this.http.post(environment.apiBaseUrl + "/Order", body);
  }
}
