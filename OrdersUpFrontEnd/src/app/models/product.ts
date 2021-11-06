import { Business } from "./business";

export class Product{
    id!: Number;
    name!: String;
    code!: String;
    businessId!: Number;
    business!: Business;
}