export class Customers {

    constructor(
        
    ) {
        this.id = 0;
        this.firstName = "";
        this.lastName = "";
        this.login = "";
        this.password = "";
        this.birthdate = new Date();
        this.addressLine1 = "";
        this.addressLine2 = "";
        this.postCode = "";
    }

    public id: number;
    public login: string;
    public password: string;
    public firstName: string;
    public lastName: string;
    public addressLine1: string;
    public addressLine2: string;
    public postCode: string;
    public birthdate: any;
}