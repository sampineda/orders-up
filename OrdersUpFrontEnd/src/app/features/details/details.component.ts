import { Component, ElementRef, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Client } from 'src/app/models/client';
import { Detail } from 'src/app/models/detail';
import { Order } from 'src/app/models/order';
import { Product } from 'src/app/models/product';
import { ClientsService } from 'src/app/services/clients.service';
import { DetailsService } from 'src/app/services/details.service';
import { InventoriesService } from 'src/app/services/inventories.service';
import { OrdersService } from 'src/app/services/orders.service';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  order !: Order;
  orders !: Order[];
  client !: Client;
  products !: Product[];
  text !: string;
  detail !: Detail;
  details !: Detail[];
  status !: Boolean;

  constructor(private _orderService: OrdersService, private _productService: ProductsService, 
    private _clientsService: ClientsService,
    private router: Router, private route: ActivatedRoute,
    private _detailService: DetailsService,
    private elementRef: ElementRef,) {
    this.order = new Order();
    this.client = new Client();
    this.detail = new Detail();
   }

  ngOnInit(): void {
    const orderId= this.route.snapshot.params['orderId'];   
    const clientId= this.route.snapshot.params['id'];
    this.getDetailsbyOrder(clientId, orderId);
    this.getOrder(orderId);

    this.order.id= orderId;
    this._clientsService.getClient(clientId).subscribe(res => {
      this.client = res;
    });
  }

  ngAfterViewInit() {
    this.elementRef.nativeElement.ownerDocument
        .body.style.backgroundColor = 'rgb(223, 116, 63, 0.733)';
}

  getDetailsbyOrder(id : Number, orderId: Number){
    this._detailService.getDetailbyOrder(id, orderId).subscribe(data => {
      this.details= data;
    })
  }

  async getOrder(orderId: Number){
    let businessInfo = await this._orderService.getOrder(orderId).toPromise();
    var status: Boolean;
    this.order.clientId=businessInfo['clientId'];
    this.order.businessId=businessInfo['businessId'];
    this.order.elaborationMinutes=businessInfo['elaborationMinutes'];
    this.order.dueDate=businessInfo['dueDate'];
    this.status= businessInfo['done'];

    if(this.status==true){
      this.text="pendiente";
    }else{
      this.text="terminado"
    }
  }

  changeStatus(){
    if(this.status==true){
    this.order.done=false;
    }else{
    this.order.done=true;
    }
    this.editOrder(this.order);
  }

  editOrder(order: Order){
    this._orderService.editOrder(this.order).subscribe(() =>{
      this.router.navigate(['/orders']);
    })
  }

  back(){
    this.router.navigate(['/orders']);
  }

}
