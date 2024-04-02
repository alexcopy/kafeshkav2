import {Routes} from '@angular/router';
import {OrderComponent} from "./orders/order.component";
import {PaymentDetailsComponent} from "./payment-details/payment-details.component";

export const routes: Routes = [
  {path: "", redirectTo: "order", pathMatch: "full"},

  {path: "payment", component: PaymentDetailsComponent},
  {path: "order", component: OrderComponent},
  {
    path: "orders", children: [
      {path: "", component: OrderComponent},
      {path: "edit:id", component: OrderComponent}
    ]
  }
];
