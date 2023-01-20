import { OrderState } from "./OrderState";

export class Orders {
    id?: number;
    customerId?: number;
    orderDate?: Date;
    shippingAddress?: string;
    shippingDate?: Date;
    state: OrderState = OrderState.Created;
    orderNumber?: string;
}