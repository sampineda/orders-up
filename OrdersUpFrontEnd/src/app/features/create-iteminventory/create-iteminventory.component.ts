import { Component, OnInit } from '@angular/core';
import { Inventory } from 'src/app/models/inventory';
import { InventoriesService } from 'src/app/services/inventories.service';
import { Router } from '@angular/router';
import { Product } from 'src/app/models/product';
import { ProductsService } from 'src/app/services/products.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create-iteminventory',
  templateUrl: './create-iteminventory.component.html',
  styleUrls: ['./create-iteminventory.component.css']
})
export class CreateIteminventoryComponent implements OnInit {
  products !: Product[];
  item !: Inventory;

  constructor(private _inventoryService: InventoriesService,
    private _productsService : ProductsService,
     private router: Router,
     private toastr: ToastrService) {
    this.item = new Inventory();
   }

  ngOnInit(): void {
    this._productsService.getProducts().subscribe(res =>{
      this.products=res;
    })
  }

  createItem(){
    this.item.businessId=1;
    if(this.item){
      this._inventoryService.createInventory(this.item).subscribe(() => {
        this.router.navigate(['/inventory']);
        this.toastr.success('Nuevo articulo agregado','Agregado Exitosamente')
      })
    }
  }

  createProduct(){
    this.router.navigate(['/product/create']);
  }

  cancel(){
    this.router.navigate(['/inventory']);
  }

}
