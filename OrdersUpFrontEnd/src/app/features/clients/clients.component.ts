import { Component, ElementRef, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Client } from 'src/app/models/client';
import { ClientsService } from 'src/app/services/clients.service';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css']
})
export class ClientsComponent implements OnInit {
clients !: Client[];

  constructor(private elementRef: ElementRef,private _clientsService: ClientsService, private router: Router) { }

  ngOnInit(): void {
    this.getClients(); 
  }

  ngAfterViewInit() {
    this.elementRef.nativeElement.ownerDocument
        .body.style.backgroundColor = 'rgb(244, 210, 172)';
}

getClients(){
  this._clientsService.getClients().subscribe(data =>{
    this.clients= data;
  })
}

editClient(id: Number){
  this.router.navigate(['/clients/edit', id]);
}

createClient(){
  this.router.navigate(['/clients/create']);
}
}
