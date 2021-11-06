import { Component, ElementRef, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Client } from 'src/app/models/client';
import { ClientsService } from 'src/app/services/clients.service';

@Component({
  selector: 'app-edit-client',
  templateUrl: './edit-client.component.html',
  styleUrls: ['./edit-client.component.css']
})
export class EditClientComponent implements OnInit {
  client !: Client;

  constructor(private elementRef: ElementRef, private _clientService: ClientsService,
    private router: Router,
    private route: ActivatedRoute) {
      this.client = new Client();
     }

  ngOnInit(): void {
    const id= this.route.snapshot.params['id'];

    this._clientService.getClient(id).subscribe(res =>{
      this.client = res;
    })
  }

  ngAfterViewInit() {
    this.elementRef.nativeElement.ownerDocument
        .body.style.backgroundColor = 'rgb(244, 210, 172)';
}

  editClient(){
    this._clientService.editClient(this.client).subscribe(() =>{
      this.router.navigate(['/clients']);
    })
  }

  cancel(){
    this.router.navigate(['/clients']);
  }

}
