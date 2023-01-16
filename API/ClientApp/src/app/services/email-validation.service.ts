import { Injectable } from "@angular/core";
import { DatabaseService } from "./database.service";

@Injectable({
    providedIn: 'root'
})
export class EmailValidationService {

    constructor(
        private dbService: DatabaseService
    ) {

    }

    validateEmail(email: string): boolean {
        var result = false;

        this.dbService.SetRoute(`auth/validateemail?email=${email}`);
        this.dbService.Get<boolean>().subscribe((data: boolean) => {
            result = data;
        }, (err) => {
            console.log(err);
        });

        return !result;
    }
}