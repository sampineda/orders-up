import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Client } from 'src/app/models/client';
import { Detail } from 'src/app/models/detail';
import { Inventory } from 'src/app/models/inventory';
import { Logo } from 'src/app/models/logo';
import { Order } from 'src/app/models/order';
import { Product } from 'src/app/models/product';
import { BusinessService } from 'src/app/services/business.service';
import { ClientsService } from 'src/app/services/clients.service';
import { DetailsService } from 'src/app/services/details.service';
import { InventoriesService } from 'src/app/services/inventories.service';
import { LogosService } from 'src/app/services/logos.service';
import { OrdersService } from 'src/app/services/orders.service';
import { ProductsService } from 'src/app/services/products.service';
import { UsersService } from 'src/app/services/users.service';
import { DatePipe } from '@angular/common'
import { EventosService } from 'src/app/services/eventos.service';
import { Eventos } from 'src/app/models/eventos';

@Component({
  selector: 'app-create-order',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.css']
})
export class CreateOrderComponent implements OnInit {
  public show: boolean= false;
  public btnCreateOrder: any= 'Show';
  clients !: Client[];
  order !: Order;
  evento !: Eventos;
  orderList !: Order;
  items !: Inventory[];
  item !: Inventory;
  detail !: Detail;
  itemsColor !: Inventory[];
  products !: Product[];
  selectedOption;
  logos !: Logo[];
  details !: Detail[];
  butDisabled!: boolean;
  heads !: Number;
  url="";
  imageUrl : string = "/assets/images/logo.png";
  dateToDeliver !: Date;
  clientName !: string;
  businessId !: Number;

  constructor(private elementRef: ElementRef, private _orderService: OrdersService, 
    private _clientService: ClientsService, private router: Router,
    private _productService: ProductsService, private _inventoryService: InventoriesService,
    private _logoService: LogosService, private _detailService: DetailsService,
    private _userService: UsersService, private toastr: ToastrService,
    private _businessService: BusinessService, public datepipe: DatePipe,
    private _eventosService: EventosService) {
      this.order = new Order();
      this.detail = new Detail();
      this.item = new Inventory();
      this.evento = new Eventos();
     }

  async ngOnInit(): Promise<void> {
    this.getInventory();
    this.getProducts();
    this.getLogos();
    this._clientService.getClients().subscribe(res =>{
      this.clients=res; 
    })

    let businessInfo = await this._userService.getBusiness().toPromise()
    
   let h= await this._businessService.getBusiness(businessInfo['businessId']).toPromise()
   console.log('heads', h['heads']);
   this.businessId= businessInfo['businessId'];
  }

  handleFileInput(e){
    if(e.target.files){
      var reader= new FileReader();
      reader.readAsDataURL(e.target.files[0]);
      reader.onload= (picture: any)=>{
        this.imageUrl= picture.target.result;
      }
    }

  }

  toggle(){
    this.show= !this.show;
  }

ngAfterViewInit() {
    this.elementRef.nativeElement.ownerDocument
        .body.style.backgroundColor = 'rgb(223, 63, 63, 0.733)';
}

onselectFile(e){
if(e.target.files){
  var reader= new FileReader();
  reader.readAsDataURL(e.target.files[0]);
  reader.onload= (picture: any)=>{
    this.url= picture.target.result;
  }
}
}

selectChangeHandler (event: any) {
  this.selectedOption = event.target.value;
  this.getInventorybyProduct(this.selectedOption, this.selectedOption);
}

async createOrder(){
  let dateTemp: Date = new Date("2019-01-16");  
    this.order.businessId= this.businessId;
    this.order.elaborationMinutes=1;
    this.order.dueDate= dateTemp;
    this.order.done=false;
    let orderdata = {}
    if(this.order){
      orderdata= await this._orderService.createOrder(this.order).toPromise()
    }
  this.detail.orderId= orderdata['id'];
  this.order.id= orderdata['id'];
}

async createList(){
  let inventoryId = (<HTMLSelectElement>document.getElementById('colorControlSelect')).value;
  let x= {};
  var quantity, newQuantity, actualQuantity, stitches=24825;
  this.detail.stitches= stitches;
  this.detail.inventoryId= Number(inventoryId);
  if(this.detail){
    quantity= Number(this.detail.quantity)
    x= await this._inventoryService.getInventory(this.detail.inventoryId).toPromise()
    actualQuantity=x['quantity'];
    this.item.color= x['color'];
    this.item.id= x['id'];
    this.item.productId= x['productId'];
    this.item.businessId=1;

    if(quantity > actualQuantity){
      this.toastr.error('La cantidad actual del producto en inventario es: '+actualQuantity, 'Falta de Inventario',{ timeOut: 9500 })
    this.reset();
    }else{
      newQuantity=actualQuantity-quantity;
      this.item.quantity= Number(newQuantity);
      this._detailService.createDetail(this.detail).subscribe(() => {
      })
      this._inventoryService.editInventory(this.item).subscribe(() => {
        this.getDetailbyOrder(1, this.detail.orderId);
      })
      this.toastr.success('Se agrego el detalle a la orden','Guardado Exitosamente')
      this.reset();
    }
  }
}


reset(){
  this.selectedOption=0;
  this.detail.logoId=0;
  this.detail.price=0;
  this.detail.quantity=0;
}

cancelOrder(){
  const res= confirm("¿Esta seguro que desea cancelar la orden?");
  if(res){
      this.router.navigate(['/orders']);
  }
}

actionMethod(event: any) {
  event.target.disabled = true;
  this.butDisabled= true;
}

getDetailbyOrder(id: Number, orderId: Number){
  this._detailService.getDetailbyOrder(id, orderId).subscribe(data =>{
    this.details= data;
  })
}

getLogos(){
  this._logoService.getLogos().subscribe(data =>{
    this.logos= data;
  })
}

getProducts(){
this._productService.getProducts().subscribe(data =>{
  this.products=data;
})
}

getInventory(){
  this._inventoryService.getInventories().subscribe(data => {
    this.items= data;
  })
}

getInventorybyProduct(id: Number, productId: Number){
  this._inventoryService.getInventorybyProduct(id, productId).subscribe(data =>{
    this.itemsColor= data;
  })
}

onChange($event){
  let text = $event.target.options[$event.target.options.selectedIndex].text;
  this.clientName= text;
  }

async calculateDate(){  
let finalOrder= {};
var dateToDeliver = new Date();
  finalOrder= await this._orderService.editOrder(this.order).toPromise()
  dateToDeliver=finalOrder['dueDate'];
  const dateFormat =this.datepipe.transform(dateToDeliver, 'EEEE, d MMMM y');
  const res= confirm("La fecha de entrega calculada es: "+ dateFormat);
  this.evento.title= 'Pedido de '+ this.clientName ;
  this.evento.start=dateToDeliver;
  this.evento.end=dateToDeliver;
  this.evento.startStr= dateToDeliver.toString();
  this.evento.endStr= dateToDeliver.toString();
  this.evento.orderId= this.detail.orderId;
  await this._eventosService.createEvento(this.evento).toPromise();

  if(res){
    this.router.navigate(['/events']);
    this.toastr.success('Se creo la orden exitosamente','Guardado Exitosamente')
  }
}

cancel(){
  this.router.navigate(['/orders']);
}

}
