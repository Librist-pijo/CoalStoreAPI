import { Injectable } from "@angular/core";
import { DatabaseService } from "./database.service";

@Injectable({
    providedIn: 'root'
})
export class EmailValidationService {

    constructor(
        public dbService: DatabaseService
    ) {

    }

    validateEmail(email: string): boolean {
        var result = false;

        this.dbService.SetRoute(`validate-email?email=${email}`);
        this.dbService.Get<boolean>().subscribe((result) => {
            result = result.valueOf();
        }, (err) => {
            console.log(err);
        });

        return result;
    }
}