import { Component, ElementRef, Inject, OnInit } from '@angular/core';
import { Order } from 'src/app/models/order';
import { OrdersService } from 'src/app/services/orders.service';
import { Router } from '@angular/router';
import { IconPrefix, IconName } from '@fortawesome/fontawesome-svg-core';
import { DetailsService } from 'src/app/services/details.service';
import { Detail } from 'src/app/models/detail';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
orders!: Order[];
allOrders !: Order[];
searchText: string = '';
collectionSize!: Number;
iconName: IconName = 'check-circle';
iconPrefix: IconPrefix = 'fas'
done !: boolean ;
details !: Detail[];

  constructor(private _ordersService: OrdersService, private _detailService: DetailsService,
    private router: Router,private toastr: ToastrService,
    private elementRef: ElementRef) {
   }
  async ngOnInit(): Promise<void> {
    this.orders = await this.getOrders();
  }

  ngAfterViewInit() {
    this.elementRef.nativeElement.ownerDocument
    .body.style.backgroundColor = 'rgb(223, 191, 63, 0.733)';
}
  
  search(value: string): void {
    this.orders = this.allOrders.filter((val) => val.client.name.toLowerCase().includes(value));
    this.orders;
    this.collectionSize = this.orders.length;
  }

  async getOrders(){
    let orders : Order[] = []
    try {
      return await this._ordersService.getOrders().toPromise()
    } catch (error) {
      console.log('error',error)
      return orders;
    }
}

reloadCurrentPage() {
  window.location.reload();
 }

deleteOrder(clientId: Number,orderId: Number){
  const res= confirm("¿Desea eliminar la orden?");
  if(res){
    this._ordersService.deleteOrder(orderId).subscribe(() => {})
    this.toastr.success('Orden eliminada exitosamente', 'Eliminada');
  }
  this.reloadCurrentPage();
  }

getDetailsByOrder(id: Number, orderId: Number){
  this.router.navigate(['details/'+ id + '/' + orderId]);
}

getOrder(id: Number){
  this.router.navigate(['orders/',id]);
}

}
