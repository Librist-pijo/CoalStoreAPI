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

    async getProducts(): Promise<Products[]> {
        var products: Products[] = [];
        this.dbService.SetRoute('products/getproducts');
        await this.dbService.Get<Products[]>().toPromise().then((result) => {
            if (result != undefined && result != null) {
                products = result;
            }
        }, (err) => {
            Swal.fire('Wystąpił błąd pobrania danych', '', 'error');
        });

        return products;
    }
}