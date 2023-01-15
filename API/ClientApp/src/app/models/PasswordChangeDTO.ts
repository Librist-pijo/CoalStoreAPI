export class PasswordChangeDTO {
    constructor(id: number, login: string) {
        this.customerId = id;
        this.customerLogin = login;
        this.oldPassword = "";
        this.newPassword = "";
        this.newPasswordRepeat = ""
    }

    customerId: number;
    customerLogin: string;
    oldPassword: string;
    newPassword: string;
    newPasswordRepeat: string;
}