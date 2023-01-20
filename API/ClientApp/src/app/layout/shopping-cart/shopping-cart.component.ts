import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { CreateOrdersDTO } from "src/app/models/CreateOrdersDTO";
import { Customers } from "src/app/models/Customers";
import { Orders } from "src/app/models/Orders";
import { OrdersProducts } from "src/app/models/OrdersProducts";
import { OrderState } from "src/app/models/OrderState";
import { Products } from "src/app/models/Products";
import { AuthService } from "src/app/services/auth.service";
import { CustomersService } from "src/app/services/customers.service";
import { OrdersService } from "src/app/services/orders.service";
import { ProductsService } from "src/app/services/products.service";
import Swal from "sweetalert2";

@Component({
    selector: 'app-shopping-cart',
    templateUrl: './shopping-cart.component.html',
    styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {
    ordersProducts: OrdersProducts[] = [];
    products: Products[] = [];
    emptyBasketImgPath = '../../../assets/img/empty-cart.svg';
    order: CreateOrdersDTO = new CreateOrdersDTO();
    totalValue: number = 0;
    customer: Customers = new Customers();

    constructor(
        public ordersService: OrdersService,
        public productsService: ProductsService,
        private router: Router,
        private authService: AuthService,
        public customersService: CustomersService
    ) {}
    
    async ngOnInit() {
        await this.getOrdersProductsWithStock();

        const login = this.authService.getLoggedUserLogin();

        if (login) {
            this.customersService.getCustomerByLogin(login).then((data) => {
                this.customer = data;
                this.order.customerId = this.customer.id;
                this.order.state = OrderState.Created;
                this.customer.password = "";
            });
        }
    }

    async getOrdersProductsWithStock() {
        this.ordersProducts = this.ordersService.getCurrentOrderProducts();
        this.products = await this.productsService.getProducts();
        this.totalValue = 0;
        
        if (this.ordersProducts && this.ordersProducts.length > 0) {
            for(var ordersProduct of this.ordersProducts) {
                ordersProduct.product = this.products.find(x => x.id == ordersProduct.productId);
                this.totalValue += ordersProduct.totalPrice;
            }
        }
    }

    navigateToProducts() {
        this.router.navigate(['./products']);
    }   

    updateProductInCart(orderProduct: OrdersProducts) {
        if (orderProduct.product) {
            this.ordersService.updateProductInCart(orderProduct.product, orderProduct.quantity);
            this.getOrdersProductsWithStock();
        }
    }

    removeProductFromCart(orderProduct: OrdersProducts) {
        if (orderProduct.productId) {
            this.ordersService.removeProductFromCart(orderProduct.productId);
            Swal.fire('Usunięto produkt ' + orderProduct.product?.name + ' z koszyka', '', 'success');
            this.getOrdersProductsWithStock();
        }
    }

    createOrder() {
        this.order.shippingAddress = this.customer.addressLine1 + " " + this.customer.addressLine2 + this.customer.postCode;
        this.order.products = this.ordersProducts;

        this.ordersService.createOrder(this.order).then((data) => {
            if (data && data.success) {
                Swal.fire('Utworzono zamówienie', '', 'success');
                this.ordersService.resetShoppingCart();
                this.getOrdersProductsWithStock();
            } else {
                Swal.fire('Nie utworzono zamówienia', data?.error ?? '', 'error');
            }
        }, (err) => {
            Swal.fire('Nie utworzono zamówienia', '', 'error');
        });
    }
}