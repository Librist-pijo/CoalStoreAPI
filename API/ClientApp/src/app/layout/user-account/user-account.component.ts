import { Component, OnInit } from "@angular/core";
import { Customers } from "src/app/models/Customers";
import { EnumDescriptionDTO } from "src/app/models/EnumDescriptionDTO";
import { Orders } from "src/app/models/Orders";
import { OrderState } from "src/app/models/OrderState";
import { PasswordChangeDTO } from "src/app/models/PasswordChangeDTO";
import { AuthService } from "src/app/services/auth.service";
import { CustomersService } from "src/app/services/customers.service";
import { OrdersService } from "src/app/services/orders.service";
import Swal from "sweetalert2";

@Component({
    selector: 'app-user-account',
    templateUrl: './user-account.component.html',
    styleUrls: ['./user-account.component.css']
})
export class UserAccountComponent implements OnInit {
    selectedItemId: number = 1;
    customer: Customers = new Customers();
    now: Date = new Date();
    passwordChange!: PasswordChangeDTO;
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
        private ordersService: OrdersService,
        private authService: AuthService,
        private customersService: CustomersService
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

    async ngOnInit() {
        this.ordersService.getOrdersStates().then((data) => {
            if (data) {
                this.ordersStates = data;
            }
        });
        
        const login = this.authService.getLoggedUserLogin();

        await this.customersService.getCustomerByLogin(login).then((data) => {
            this.customer = data;
            this.customer.password = "";
            
            this.passwordChange = new PasswordChangeDTO(this.customer.id, this.customer.login);
        });

        await this.ordersService.getOrdersByCustomerId(this.customer.id).then((data) => {
            if (data)
                this.customersOrders = data;
        });
    }

    onSelectionChanged(e: any) {
        if (e.addedItems) {
            this.selectedItemId = e.addedItems[0].id;
        }
    }

    saveCustomerPersonalData(e: any) {
        this.customersService.updateCustomer(this.customer).then((result) => {
            if (result && result.success) {
                Swal.fire('Twoje dane zostały zaktualizowane!', '', 'success');

                if (result.data) {
                    this.customer = result.data;
                    this.customer.password = "";
                }
            } else {
                Swal.fire('Aktualizacja danych nie powiodła się', '', 'error');
            }
        }, (err) => {
            Swal.fire('Aktualizacja danych nie powiodła się', '', 'error');
        });
    }

    changeCustomerPassword(e: any) {
        this.customersService.updateCustomerPassword(this.passwordChange).then((result) => {
            if (result && result.success) {
                Swal.fire('Twoje hasło zostało zaktualizowane!', '', 'success');

                if (result.data) {
                    this.customer = result.data;
                    this.customer.password = "";
                }
            } else {
                Swal.fire('Aktualizacja hasła nie powiodła się', result.error ?? '', 'error');
            }
        }, (err) => {
            Swal.fire('Aktualizacja hasła nie powiodła się', '', 'error');
        });
    }
}