import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Logo } from '../models/logo';

@Injectable({
  providedIn: 'root'
})
export class LogosService {
  apiURL="https://localhost:44377/api/logo";
  
  constructor(private http: HttpClient) { }

  getLogos(){
    return this.http.get<Logo[]>(this.apiURL);
  }

  getLogo(id: Number){
    return this.http.get<Logo>(this.apiURL+ '/' + id);
  }

  createLogo(logo: Logo){
    return this.http.post<Logo>(this.apiURL, logo);
  }
}
