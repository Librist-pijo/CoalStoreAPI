import { Component, OnInit } from "@angular/core";
import { Products } from "src/app/models/Products";
import { OrdersService } from "src/app/services/orders.service";
import { ProductsService } from "src/app/services/products.service";
import Swal from "sweetalert2";

@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
    products: Products[] = [];

    constructor(
        private productsService: ProductsService,
        private ordersService: OrdersService
    ) {}

    async ngOnInit() {
        this.productsService.getProducts().then((data) => {
            this.products = data;

            for(var product of this.products) {
                if (product.stock > 0) {
                    product.quantity = 1;
                }
            }
        });
    }

    addProductToCart(e: any, product: Products) {
        this.ordersService.addProductToOrder(product, product.quantity);
        product.quantity = 1;
        Swal.fire('Dodano produkt do koszyka!', '', 'success');
    }
}