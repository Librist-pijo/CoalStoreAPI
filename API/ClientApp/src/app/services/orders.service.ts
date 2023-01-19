import { Injectable } from "@angular/core";
import { CreateOrdersDTO } from "../models/CreateOrdersDTO";
import { EnumDescriptionDTO } from "../models/EnumDescriptionDTO";
import { Orders } from "../models/Orders";
import { OrdersProducts } from "../models/OrdersProducts";
import { OrderState } from "../models/OrderState";
import { Products } from "../models/Products";
import { ResultData } from "../models/ResultData";
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

        this.dbService.SetRoute('orders/getordersstates');
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
            newOrderProduct.totalPrice = quantity * product.price;
            newOrderProduct.product = product;
            ordersProducts.push(newOrderProduct);
        }

        this.saveOrdersProducts(ordersProducts);
    }

    saveOrdersProducts(ordersProducts: OrdersProducts[]) {
        localStorage.setItem(CURRENT_ORDER_PRODUCTS, JSON.stringify(ordersProducts));
    }

    resetShoppingCart() {
        localStorage.removeItem(CURRENT_ORDER_PRODUCTS);
    }

    removeProductFromCart(productId: number) {
        var ordersProducts = this.getCurrentOrderProducts();var orderProductIndex = ordersProducts.findIndex(x => x.productId == productId);
        
        if (orderProductIndex > -1) {
            ordersProducts.splice(orderProductIndex, 1);
        }

        this.saveOrdersProducts(ordersProducts);
    }

    updateProductInCart(product: Products, quantity: number = 1) {
        if (quantity > 0) {
            var ordersProducts = this.getCurrentOrderProducts();
    
            var orderProductIndex = ordersProducts.findIndex(x => x.productId == product.id);
            if (orderProductIndex > -1) {
                ordersProducts[orderProductIndex].quantity = quantity;
                ordersProducts[orderProductIndex].totalPrice = ordersProducts[orderProductIndex].quantity * product.price;
            }
            
            this.saveOrdersProducts(ordersProducts);
        } else {
            this.removeProductFromCart(product.id);
        }
    }

    async createOrder(order: CreateOrdersDTO): Promise<ResultData | undefined> {
        this.dbService.SetRoute('orders/createorder');
        return await this.dbService.Add<ResultData>(order).toPromise().finally();
    }
}