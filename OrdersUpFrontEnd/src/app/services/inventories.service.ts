import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Inventory } from '../models/inventory';

@Injectable({
  providedIn: 'root'
})
export class InventoriesService {
  apiURl='https://localhost:44377/api/inventory';

  constructor(private http: HttpClient) { }

  getInventories(){
    return this.http.get<Inventory[]>(this.apiURl);
  }

  getInventorybyProduct(id: Number, productId: Number){
    return this.http.get<Inventory[]>(this.apiURl+ '/' + id + '/' + productId);
  }

  getInventory(id: Number){
    return this.http.get<Inventory>(this.apiURl+ '/' + id);
  }

  createInventory(inventory: Inventory){
    return this.http.post<Inventory>(this.apiURl, inventory);
  }

  editInventory(inventory: Inventory){
    return this.http.put<Inventory>(this.apiURl + "/" + inventory.id, inventory);
  }
}
