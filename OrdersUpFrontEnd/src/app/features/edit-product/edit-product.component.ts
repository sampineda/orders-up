import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from 'src/app/models/product';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit {
product !: Product;

  constructor(private _productsService: ProductsService, private router: Router,
    private route: ActivatedRoute) {
      this.product = new Product();
    }

  ngOnInit(): void {
    const id= this.route.snapshot.params['id'];

    this._productsService.getProduct(id).subscribe(res => {
      this.product = res;
    });  
  }

  editProduct(){
    this._productsService.editProduct(this.product).subscribe(() => {
      this.router.navigate(['/products']);
    })
  }

  cancel(){
    this.router.navigate(['/products']);
  }
}
