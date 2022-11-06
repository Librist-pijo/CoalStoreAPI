import { InvoiceState } from "./InvoiceState";

export class Invoices {
    id?: number;
    orderId?: number;
    paymentMethodId?: number;
    state?: InvoiceState;
    amount?: number;
    price?: number;
}