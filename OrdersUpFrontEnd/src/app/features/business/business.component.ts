import { Component, ElementRef, OnInit } from '@angular/core';
import { Business } from 'src/app/models/business';
import { Machine } from 'src/app/models/machine';
import { BusinessService } from 'src/app/services/business.service';
import { MachinesService } from 'src/app/services/machines.service';
import { UsersService } from 'src/app/services/users.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-business',
  templateUrl: './business.component.html',
  styleUrls: ['./business.component.css']
})
export class BusinessComponent implements OnInit {
businessId !: Number;
business !: Business;
machines !: Machine[];
machine !: Machine;

  constructor(private elementRef: ElementRef, private _userService: UsersService,
    private _businessService: BusinessService, private _machinesService: MachinesService,
    private toastr: ToastrService, private router: Router) {
      this.machine= new Machine();
      this.business= new Business();
     }

  async ngOnInit(): Promise<void> {
    let businessInfo = await this._userService.getBusiness().toPromise()
   this.businessId= businessInfo['businessId'];

   this.getBusiness(this.businessId);
   this.getMachineByBusinessId(this.businessId,this.businessId);
  }

  ngAfterViewInit() {
    this.elementRef.nativeElement.ownerDocument
        .body.style.backgroundColor = 'rgb(174, 96, 67, 0.733)';
}

getBusiness(id: Number){
this._businessService.getBusiness(id).subscribe(data => {
  this.business= data;
})
}

getMachineByBusinessId(id: Number, businessId: Number){
  this._machinesService.getMachineByBusinessId(id, businessId).subscribe(data => {
    this.machines= data;
  })
}

deleteMachine(id: Number){
const res= confirm("¿Desea eliminar la maquina?");
if(res){
  this._machinesService.deleteMachine(id).subscribe(() => {
    this.getMachineByBusinessId(this.businessId,this.businessId);
  })
}
}

createMachine(){
  this.machine.businessId= this.businessId;
  if(this.machine.heads<=0){
    this.toastr.error('Debe de ingresar una cantidad de cabezas valido.', 'Error');
  }else{  
    this._machinesService.createMachine(this.machine).subscribe(data =>{
      this.toastr.success('Nueva maquina agregada','Agregado Exitosamente');
      this.getMachineByBusinessId(this.businessId,this.businessId);
    }
  )
  }
  this.machine.heads=0;
}

}
