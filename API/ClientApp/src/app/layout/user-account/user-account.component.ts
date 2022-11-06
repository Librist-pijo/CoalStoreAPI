import { Component, OnInit } from "@angular/core";
import { Customers } from "src/app/models/Customers";
import { EnumDescriptionDTO } from "src/app/models/EnumDescriptionDTO";
import { Orders } from "src/app/models/Orders";
import { OrderState } from "src/app/models/OrderState";
import { PasswordChangeDTO } from "src/app/models/PasswordChangeDTO";
import { OrdersService } from "src/app/services/orders.service";

@Component({
    selector: 'app-user-account',
    templateUrl: './user-account.component.html',
    styleUrls: ['./user-account.component.css']
})
export class UserAccountComponent implements OnInit {
    selectedItemId: number = 1;
    customer: Customers = new Customers();
    now: Date = new Date();
    passwordChange: PasswordChangeDTO = new PasswordChangeDTO();
    passwordMode = 'password';
    passwordRepeatMode = 'password';
    passwordButton: any;
    passwordRepeatButton: any;
    oldPasswordMode = "password";
    oldPasswordButton: any;
    customersOrders: Orders[] = [];
    ordersStates: EnumDescriptionDTO<OrderState>[] = [];

    userAccountTabs: any[] = [
        { id: 1, text: 'Moje dane', icon: 'user' },
        { id: 2, text: 'Moje zamówienia', icon: 'bulletlist' },
        { id: 3, text: 'Bezpieczeństwo', icon: 'key' }
    ];

    constructor(
        private ordersService: OrdersService
    ) {
        this.passwordButton = {
            icon: "visibility",
            stylingMode: 'text',
            onClick: () => {
                if (this.passwordMode == "text") {
                    this.passwordMode = "password";
                } else {
                    this.passwordMode = "text";
                }
            }
        };
        
        this.passwordRepeatButton = {
            icon: "visibility",
            stylingMode: 'text',
            onClick: () => {
                if (this.passwordRepeatMode == "text") {
                    this.passwordRepeatMode = "password";
                } else {
                    this.passwordRepeatMode = "text";
                }
            }
        }
        
        this.oldPasswordButton = {
            icon: "visibility",
            stylingMode: 'text',
            onClick: () => {
                if (this.oldPasswordMode == "text") {
                    this.oldPasswordMode = "password";
                } else {
                    this.oldPasswordMode = "text";
                }
            }
        }
    }

    ngOnInit(): void {
        this.ordersStates = this.ordersService.getOrdersStates();
    }

    onSelectionChanged(e: any) {
        if (e.addedItems) {
            this.selectedItemId = e.addedItems[0].id;
        }
    }

    saveCustomerPersonalData(e: any) {
         
    }

    changeCustomerPassword(e: any) {
        
    }
}