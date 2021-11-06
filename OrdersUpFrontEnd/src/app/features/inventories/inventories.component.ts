import { Component, ElementRef, OnInit } from '@angular/core';
import { Inventory } from 'src/app/models/inventory';
import { InventoriesService } from 'src/app/services/inventories.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inventories',
  templateUrl: './inventories.component.html',
  styleUrls: ['./inventories.component.css']
})
export class InventoriesComponent implements OnInit {
inventories !: Inventory[];

  constructor(private elementRef: ElementRef,private _inventoriesService: InventoriesService, private router: Router) { }

  ngOnInit(): void {
    this.getItems(); 
  }

  ngAfterViewInit() {
    this.elementRef.nativeElement.ownerDocument
        .body.style.backgroundColor = 'rgb(63, 180, 223, 0.733)';
}

  getItems(){
    this._inventoriesService.getInventories().subscribe(data =>{
      this.inventories = data;
    })
  }

  createItem(){
    this.router.navigate(['/inventory/create']);
  }

  editItem(id: Number){
    this.router.navigate(['/inventory/edit', id]);
  }

}
