import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  apiURl='https://localhost:44377/api/product';

  constructor(private http: HttpClient) { }

  getProducts(){
    return this.http.get<Product[]>(this.apiURl);
  }

  getProduct(id: Number){
    return this.http.get<Product>(this.apiURl+ '/' + id);
  }

  createProduct(product: Product){
    return this.http.post<Product>(this.apiURl, product);
  }

  editProduct(product: Product){
    return this.http.put<Product>(this.apiURl + "/" + product.id, product);
  }
}
