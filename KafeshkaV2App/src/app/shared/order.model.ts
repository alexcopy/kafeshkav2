export class Order {
  orderId: number = 0;
  orderNo: number = 0;
  customer?: any;
  customerId: number = 0;
  pMethod: string = "";
  gTotal: number = 0;
  deletedOrderItemIds: string = ""
}
