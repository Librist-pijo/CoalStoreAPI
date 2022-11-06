import { Injectable } from "@angular/core";
import { EnumDescriptionDTO } from "../models/EnumDescriptionDTO";
import { OrderState } from "../models/OrderState";
import { DatabaseService } from "./database.service";

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
}