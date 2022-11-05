export class RegisterDTO {
    
    constructor() {
        this.firstname = "";
        this.lastname = "";
        this.email = "";
        this.password = "";
        this.passwordRepeat = "";
        this.birthdate = new Date();
    }

    firstname: string;
    lastname: string;
    email: string;
    password: string;
    passwordRepeat: string;
    birthdate: any;
}