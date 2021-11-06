import { Component, ElementRef, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Client } from 'src/app/models/client';
import { ClientsService } from 'src/app/services/clients.service';

@Component({
  selector: 'app-create-client',
  templateUrl: './create-client.component.html',
  styleUrls: ['./create-client.component.css']
})
export class CreateClientComponent implements OnInit {
  client !: Client;

  constructor(private _clientService: ClientsService,
     private router: Router,
     private toastr: ToastrService,
     private elementRef: ElementRef) {
       this.client= new Client();
      }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
    this.elementRef.nativeElement.ownerDocument
        .body.style.backgroundColor = 'rgb(244, 210, 172)';
}

  createItem(){
    if(this.client){
      this._clientService.createClient(this.client).subscribe(() => {
        this.router.navigate(['/clients']);
        this.toastr.success('Nuevo cliente agregado','Agregado Exitosamente')
      })
    }
  }

  cancel(){
    this.router.navigate(['/clients']);
  }

}
