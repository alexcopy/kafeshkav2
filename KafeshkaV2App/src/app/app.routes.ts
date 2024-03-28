import {Routes} from '@angular/router';
import {OrdersComponent} from "./orders/orders.component";
import {PaymentDetailsComponent} from "./payment-details/payment-details.component";

export const routes: Routes = [
  {path: "", redirectTo: "order", pathMatch: "full"},

  {path: "payment", component: PaymentDetailsComponent},
  {path: "orders", component: OrdersComponent},
  {
    path: "order", children: [
      {path: "", component: OrdersComponent},
      {path: "edit:id", component: OrdersComponent}
    ]
  }
];
