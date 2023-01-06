import { Injectable } from "@angular/core";
import { EnumDescriptionDTO } from "../models/EnumDescriptionDTO";
import { Orders } from "../models/Orders";
import { OrdersProducts } from "../models/OrdersProducts";
import { OrderState } from "../models/OrderState";
import { Products } from "../models/Products";
import { DatabaseService } from "./database.service";

const CURRENT_ORDER = 'current_order';
const CURRENT_ORDER_PRODUCTS = 'current_order_products';

@Injectable({
    providedIn: 'root'
})
export class OrdersService {

    constructor(
        private dbService: DatabaseService
    ) {

    }

    getOrdersStates(): EnumDescriptionDTO<OrderState>[] {
        var enumDescriptionDTO: EnumDescriptionDTO<OrderState>[] = [];

        this.dbService.SetRoute('orders/get-orders-states');
        this.dbService.Get<EnumDescriptionDTO<OrderState>[]>().subscribe((result) => {
            enumDescriptionDTO = result;
        }, (err) => {
            console.log(err);
        });

        return enumDescriptionDTO;
    }

    getCurrentOrderProducts(): OrdersProducts[] {
        var orderProductsJson = localStorage.getItem(CURRENT_ORDER_PRODUCTS);

        if (orderProductsJson)
            return JSON.parse(orderProductsJson);
        else
            return [];
    }

    addProductToOrder(product: Products, quantity: number = 1) {
        var ordersProducts = this.getCurrentOrderProducts();

        var orderProductIndex = ordersProducts.findIndex(x => x.productId == product.id);
        if (orderProductIndex > -1) {
            ordersProducts[orderProductIndex].quantity += quantity;
            ordersProducts[orderProductIndex].totalPrice = ordersProducts[orderProductIndex].quantity * product.price;
        } else {
            var newOrderProduct: OrdersProducts = new OrdersProducts(); 
            newOrderProduct.productId = product.id;
            newOrderProduct.quantity = quantity;
            newOrderProduct.totalPrice = quantity * product.price
            ordersProducts.push(newOrderProduct);
        }

        this.saveOrdersProducts(ordersProducts);
    }

    saveOrdersProducts(ordersProducts: OrdersProducts[]) {
        localStorage.setItem(CURRENT_ORDER_PRODUCTS, JSON.stringify(ordersProducts));
    }
}