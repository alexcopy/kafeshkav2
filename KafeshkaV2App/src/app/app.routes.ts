import {Routes} from '@angular/router';
import {OrderComponent} from "./orders/order.component";
import {PaymentDetailsComponent} from "./payment-details/payment-details.component";
import {OrdersComponent} from "./orders/order/orders.component";

export const routes: Routes = [
  {path: "", redirectTo: "order", pathMatch: "full"},

  {path: "payment", component: PaymentDetailsComponent},
  {path: "orders", component: OrdersComponent},
  {
    path: "order", children: [
      {path: "", component: OrderComponent},
      {path: "edit/:id", component: OrderComponent},
      // {path: ":id", component: OrderComponent},
    ]
  },
  {
    path: "orders", children: [
      {path: "", component: OrdersComponent},
    ]
  }
];
