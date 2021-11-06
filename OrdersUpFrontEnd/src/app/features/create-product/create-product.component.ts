import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { Product } from 'src/app/models/product';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css']
})
export class CreateProductComponent implements OnInit {
  product !: Product;

  constructor(private _productService: ProductsService, private router: Router){ 
    this.product = new Product();}
    
  ngOnInit(): void {
  }

  createProduct(){
    this.product.businessId=1;
    if(this.product){
      this._productService.createProduct(this.product).subscribe(() => {
        this.router.navigate(['/inventory/create']);
      })
    }
  }

  cancel(){
    this.router.navigate(['/inventory/create']);
  }

}

