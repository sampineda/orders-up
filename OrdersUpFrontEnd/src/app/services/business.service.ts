import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Business } from '../models/business';

@Injectable({
  providedIn: 'root'
})
export class BusinessService {
  apiURL= "https://localhost:44377/api/business";

  constructor(private http: HttpClient) { }

  getBusiness(id: Number){
    return this.http.get<Business>(this.apiURL+ '/' + id);
  }
}
