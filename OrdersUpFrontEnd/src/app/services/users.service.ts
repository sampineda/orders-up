import { HttpClient, HttpHeaderResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  apiURl='https://localhost:44377/api/user';

  constructor(private http: HttpClient) { }

  login(user: User){
   return this.http.post(this.apiURl + "/login", user);
  }

  getBusiness(){
    return this.http.get(this.apiURl+ "/businessInfo");
  }
}
