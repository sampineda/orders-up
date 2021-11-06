import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Client } from '../models/client';

@Injectable({
  providedIn: 'root'
})
export class ClientsService {
  apiURL= "https://localhost:44377/api/client";

  constructor(private http: HttpClient) { }

  getClient(id: Number){
    return this.http.get<Client>(this.apiURL+ '/' + id);
  }

  getClients(){
    return this.http.get<Client[]>(this.apiURL);
  }

  editClient(client : Client){
    return this.http.put<Client>(this.apiURL + "/" + client.id, client);
  }

  createClient(client: Client){
    return this.http.post<Client>(this.apiURL, client);
  }
}
