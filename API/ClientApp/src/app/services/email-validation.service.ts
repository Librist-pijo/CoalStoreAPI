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

        this.dbService.SetRoute(`customers/validate-email?email=${email}`);
        this.dbService.Get<boolean>().subscribe((result) => {
            result = result.valueOf();
        }, (err) => {
            console.log(err);
        });

        return result;
    }
}