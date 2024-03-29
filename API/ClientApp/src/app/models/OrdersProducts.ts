import { Products } from "./Products";

export class OrdersProducts {
    id?: number;
    productId?: number;
    orderId?: number;
    quantity: number = 1;
    totalPrice: number = 0;
    product?: Products;
}