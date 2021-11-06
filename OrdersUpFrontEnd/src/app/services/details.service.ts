import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Detail } from '../models/detail';

@Injectable({
  providedIn: 'root'
})
export class DetailsService {
apiURL="https://localhost:44377/api/detail";

constructor(private http: HttpClient) { }

  getDetails(){
    return this.http.get<Detail[]>(this.apiURL);
  }

  getDetail(id: Number){
    return this.http.get<Detail>(this.apiURL+ '/' + id);
  }

  getDetailbyOrder(id: Number, orderId: Number){
    return this.http.get<Detail[]>(this.apiURL+ '/'+id+'/'+orderId)
  }

  createDetail(detail: Detail){
    return this.http.post<Detail>(this.apiURL, detail);
  }

  deleteOrder(id: Number){
    return this.http.delete(this.apiURL+ "/" + id)
  }

  /*deleteDetails(details: Detail[]){
    return this.http.delete(this.apiURL+ "/" + details)
  }*/

  deleteDetail(id: Number){
    return this.http.delete(this.apiURL + "/" + id);
  }

  editDetail(detail: Detail){
    return this.http.put<Detail>(this.apiURL + "/" + detail.id, detail);
  }

}
