export class ResultData<T=any> {

    constructor() {
        this.success = false;
    }

    success: boolean;
    error?: string;
    info?: string;
    data?: T;
}