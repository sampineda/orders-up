import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Machine } from '../models/machine';

@Injectable({
  providedIn: 'root'
})
export class MachinesService {
  apiURL="https://localhost:44377/api/machine";

  constructor(private http: HttpClient) { }

  getMachineByBusinessId(id: Number, businessId: Number){
    return this.http.get<Machine[]>(this.apiURL+ '/'+id+'/'+businessId)
  }

  deleteMachine(id: Number){
    return this.http.delete(this.apiURL + "/" + id);
  }

  createMachine(machine: Machine){
    return this.http.post<Machine>(this.apiURL, machine);
  }
}
