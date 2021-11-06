import { Logo } from "./logo";
import { Inventory } from "./inventory";
import { Order } from "./order";

export class Detail{
    id!: Number;
    orderId !: Number;
    inventoryId !: Number;
    quantity !: Number;
    price !: Number;
    logoId !: Number;
    stitches !: Number;
    logo !: Logo;
    inventory !: Inventory;
    order !: Order;
}