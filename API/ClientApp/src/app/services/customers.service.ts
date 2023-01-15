import { Injectable } from "@angular/core";
import Swal from "sweetalert2";
import { Customers } from "../models/Customers";
import { PasswordChangeDTO } from "../models/PasswordChangeDTO";
import { ResultData } from "../models/ResultData";
import { DatabaseService } from "./database.service";

@Injectable({
    providedIn: 'root'
})
export class CustomersService {
    
    constructor(
        private dbService: DatabaseService
    ) {

    }

    async getCustomerById(id: number): Promise<Customers> {
        var customer: Customers = new Customers();

        this.dbService.SetRoute(`customers/getcustomerbyid?id=${id}`);
        await this.dbService.Get<Customers>().toPromise().then((result) => {
            if (result != undefined && result != null) {
                customer = result;
            }
        }, (err) => {
            Swal.fire('Wystąpił błąd pobrania danych', '', 'error');
        });
        
        return customer;
    }

    async getCustomerByLogin(login: string): Promise<Customers> {
        var customer: Customers = new Customers();

        this.dbService.SetRoute(`customers/getcustomerbylogin?login=${login}`);
        await this.dbService.Get<Customers>().toPromise().then((result) => {
            if (result != undefined && result != null) {
                customer = result;
            }
        }, (err) => {
            Swal.fire('Wystąpił błąd pobrania danych', '', 'error');
        });
        
        return customer;
    }

    async updateCustomer(customer: Customers): Promise<ResultData<Customers>> {
        var resultData = new ResultData<Customers>();
        resultData.success = false;

        this.dbService.SetRoute('customers/updatecustomer');  
        await this.dbService.Update<ResultData<Customers>>(customer).toPromise().then((result) => {
            if (result !== undefined && result !== null)
                resultData = result;
        });
        
        return resultData;
    }

    async updateCustomerPassword(passwordChangeDTO: PasswordChangeDTO): Promise<ResultData<Customers>> {
        var resultData = new ResultData<Customers>();
        resultData.success = false;

        this.dbService.SetRoute('customers/updatecustomerpassword');  
        await this.dbService.Update<ResultData<Customers>>(passwordChangeDTO).toPromise().then((result) => {
            if (result !== undefined && result !== null)
                resultData = result;
        });
        
        return resultData;
    }
}