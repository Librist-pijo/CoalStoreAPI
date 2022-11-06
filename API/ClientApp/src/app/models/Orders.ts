import { OrderState } from "./OrderState";

export class Orders {
    id?: number;
    customerId?: number;
    orderedDate?: Date;
    shippingAddress?: string;
    shippingDate?: Date;
    state: OrderState = OrderState.Created;
    orderNumber?: string;
}