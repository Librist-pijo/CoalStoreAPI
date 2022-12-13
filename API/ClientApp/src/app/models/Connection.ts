export class Connection {
    private url: string;

    constructor() {
        this.url = 'http://localhost:7197/api/'
    }

    GetUrl(): string {
        return this.url;
    }
}
