export class RegisterDTO {
    
    constructor() {
        this.firstname = "";
        this.lastname = "";
        this.email = "";
        this.password = "";
        this.passwordRepeat = "";
        this.birthdate = new Date();
        this.addressLine1 = "";
        this.addressLine2 = "";
        this.postCode = "";
    }

    firstname: string;
    lastname: string;
    email: string;
    password: string;
    passwordRepeat: string;
    birthdate: any;
    addressLine1: string;
    addressLine2: string;
    postCode: string;
}