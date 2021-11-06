import { Business } from "./business";
import { Product } from "./product";

export class Inventory{
    id !: Number;
    productId !: Number;
    color !: String;
    quantity !: Number;
    businessId !: Number;
    product !: Product;
    business !: Business;
} 