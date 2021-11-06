import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Eventos } from '../models/eventos';

@Injectable({
  providedIn: 'root'
})
export class EventosService {
apiURL='https://localhost:44377/api/event';

  constructor(private http: HttpClient) { }

  getEventos(){
    return this.http.get<Eventos[]>(this.apiURL);
  }

  createEvento(evento: Eventos){
    return this.http.post<Eventos>(this.apiURL, evento);
  }

}
