import { Component, OnInit } from "@angular/core";
import { LoginDTO } from "src/app/models/LoginDTO";
import { RegisterDTO } from "src/app/models/RegisterDTO";
import { DatabaseService } from "src/app/services/database.service";
import { EmailValidationService } from "src/app/services/email-validation.service";

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    loginDTO: LoginDTO = new LoginDTO();
    registerDTO: RegisterDTO = new RegisterDTO();
    now: Date = new Date();
    passwordMode = 'password';
    passwordRepeatMode = 'password';
    passwordButton: any;
    passwordRepeatButton: any;

    constructor(
        private databaseService: DatabaseService,
        private emailValidationServcie: EmailValidationService
    ) {
        this.asyncValidation = this.asyncValidation.bind(this);
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
    }

    ngOnInit(): void {
        
    }

    login(e: any) {
        // TO DO 
    }

    register(e: any) {
        // TO DO
    }

    asyncValidation(params: any) {
        return this.emailValidationServcie.validateEmail(params.value);
    }
}