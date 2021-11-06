import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
// import { Detail } from '../models/detail';
import { Order } from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
apiURL='https://localhost:44377/api/order';

constructor(private http: HttpClient) { }

  getOrders(){
    return this.http.get<Order[]>(this.apiURL);
  }

  getOrder(id: Number){
    return this.http.get<Order>(this.apiURL+ '/' + id);
  }

  getOrderDetail(dueDate: Date, clientId: Number){
    return this.http.get<Order[]>(this.apiURL+ '/' + dueDate + '/' + clientId);
  }

  createOrder(order: Order){
    return this.http.post<Order>(this.apiURL, order);
  }
  
   editOrder(order: Order){
     return this.http.put<Order>(this.apiURL + "/" + order.id, order);
   }

  deleteOrder(id: Number){
    return this.http.delete(this.apiURL+ "/" + id)
  }
}
