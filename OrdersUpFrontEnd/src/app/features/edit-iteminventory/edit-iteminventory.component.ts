import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Inventory } from 'src/app/models/inventory';
import { Product } from 'src/app/models/product';
import { InventoriesService } from 'src/app/services/inventories.service';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-edit-iteminventory',
  templateUrl: './edit-iteminventory.component.html',
  styleUrls: ['./edit-iteminventory.component.css']
})
export class EditIteminventoryComponent implements OnInit {
  butDisabled: boolean = true;
  products !: Product[];
  item !: Inventory;

  constructor(private _productsService: ProductsService, 
    private _inventoryService: InventoriesService,
    private router: Router,
    private route: ActivatedRoute) {
      this.item = new Inventory();
     }

  ngOnInit(): void {
    const id= this.route.snapshot.params['id'];

    this._productsService.getProducts().subscribe(res =>{
      this.products = res;
    });

    this._inventoryService.getInventory(id).subscribe(res =>{
      this.item = res;
    })
  }

  editItem(){
    this.item.businessId=1;
    this._inventoryService.editInventory(this.item).subscribe(() =>{
      this.router.navigate(['/inventory']);
    })
  }

  cancel(){
    this.router.navigate(['/inventory']);
  }

}
