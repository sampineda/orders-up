import { Business } from "./business";
import { Client } from "./client";

export class Order{
    id!: Number;
    clientId !: Number;
    businessId !: Number;
    dueDate !: Date;
    elaborationMinutes !: Number;
    done !: Boolean;
    client !: Client;
    business !: Business;
}