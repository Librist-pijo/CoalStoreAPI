import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "src/app/services/auth.service";
import { LoginDTO } from "src/app/models/LoginDTO";
import { RegisterDTO } from "src/app/models/RegisterDTO";
import { DatabaseService } from "src/app/services/database.service";
import { EmailValidationService } from "src/app/services/email-validation.service";
import { ResultData } from "src/app/models/ResultData";
import Swal from "sweetalert2";

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
        private authService: AuthService,
        private emailValidationServcie: EmailValidationService,
        private router: Router
    ) {
        this.passwordValidation = this.passwordValidation.bind(this);
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
        this.authService.login(this.loginDTO)
        .subscribe((result: ResultData<LoginDTO>) => {
            if (result.success && result.data) {
                this.router.navigate(['./']);
            } else {
                Swal.fire('Logowanie nie powiodło się', result.error, 'error');
            }
        }, (err: any) => {
          console.log(err);
          Swal.fire('Logowanie nie powiodło się', '', 'error');
        });
    }

    register(e: any) {
        this.authService.register(this.registerDTO).subscribe((result: ResultData) => {
            if (result.success) {
                Swal.fire('Utworzono konto w systemie', 'Zaloguj się, aby korzystać ze swojego konta w systemie!', 'success');
            } else {
                Swal.fire('Rejestracja nie powiodła się', result.error, 'error');
            }
        }, (err) => {
            console.log(err);
            Swal.fire('Rejestracja nie powiodła się', '', 'error');
        })
    }

    asyncValidation(params: any) {
        return this.emailValidationServcie.validateEmail(params.value);
    }

    passwordValidation(params: any) {
        return this.registerDTO.password == this.registerDTO.passwordRepeat;
    }
}