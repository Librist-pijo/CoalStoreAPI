import { GrantType } from "./Enums/GrantType";

export class LoginDTO {

    constructor() {
        this.login = "";
        this.password = "";
        this.grantType = GrantType.Password;
    }

    login: string;
    password: string;
    grantType: GrantType;
    accessToken?: string = "";
    refreshToken?: string = "";
}