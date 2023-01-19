import { OrdersProducts } from "./OrdersProducts";
import { OrderState } from "./OrderState";

export class CreateOrdersDTO {
    customerId?: number;
    products: OrdersProducts[] = [];
    shippingAddress: string = "";
    state: OrderState = OrderState.Created;
}