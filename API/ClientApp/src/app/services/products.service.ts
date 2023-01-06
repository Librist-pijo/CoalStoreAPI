import { Injectable } from "@angular/core";
import Swal from "sweetalert2";
import { Products } from "../models/Products";
import { DatabaseService } from "./database.service";

@Injectable({
    providedIn: 'root'
})
export class ProductsService {

    constructor(
        private dbService: DatabaseService
    ) {

    }

    getProducts(): Products[] {
        var products: Products[] = [];
        this.dbService.SetRoute('products/get-products');
        this.dbService.Get<Products[]>().subscribe((result: Products[]) => {
            products = result;
        }, (err) => {
            Swal.fire('Wystąpił błąd pobrania danych', '', 'error');
        });

        return products;
    }
}