export class Connection {
    private url: string;

    constructor() {
        this.url = 'http://localhost:5001/api/'
    }

    GetUrl(): string {
        return this.url;
    }
}
