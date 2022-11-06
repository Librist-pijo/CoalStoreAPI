export class Customers {

    constructor(
        
    ) {
        this.firstname = "";
        this.lastname = "";
        this.login = "";
        this.password = "";
        this.birthdate = new Date();
        this.addressLine1 = "";
        this.addressLine2 = "";
        this.postCode = "";
    }

    private id?: number;
    public login: string;
    public password: string;
    public firstname: string;
    public lastname: string;
    public addressLine1: string;
    public addressLine2: string;
    public postCode: string;
    public birthdate: any;
}