export class PasswordChangeDTO {
    constructor() {
        this.oldPassword = "";
        this.newPassword = "";
        this.newPasswordRepeat = ""
    }

    oldPassword: string;
    newPassword: string;
    newPasswordRepeat: string;
}