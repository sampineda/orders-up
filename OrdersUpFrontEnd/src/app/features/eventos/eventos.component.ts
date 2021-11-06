import { Component, ElementRef, OnInit } from '@angular/core';
import { CalendarOptions } from '@fullcalendar/angular';
import { Eventos } from 'src/app/models/eventos';
import { EventosService } from 'src/app/services/eventos.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  eventos!: Eventos[];
  titleEvent !: string;
  defaultEvents = [
    {
      // Just an event
      title: this.titleEvent,
      start: '2021-10-10',
      className: 'scheduler_basic_event',
      color: 'red'
    },
    {
      // Just an event
      title: 'Test Event 2',
      start: '2021-10-10',
      className: 'scheduler_basic_event',
      color: 'green'
    }
  ];
  
  calendarOptions: CalendarOptions = {
    initialView: 'dayGridMonth',
    eventColor: '#174EA6',
    defaultAllDay: true,
    //eventSources:[this.defaultEvents],
    events: 'https://localhost:44377/api/event',
    eventClick: function(info){
      if(confirm('¿Marcar el ' + info.event.title + ' como completado?')){
        this.eventColor= '#56C750';
      }
    },
    contentHeight: 700
  };

  constructor(private _eventosService: EventosService,
    private elementRef: ElementRef) { }

  ngOnInit(): void {
    this.getEventos();
  }

  ngAfterViewInit() {
    this.elementRef.nativeElement.ownerDocument
        .body.style.backgroundColor = 'rgb(223, 63, 63, 0.733)';
}

  /*getEventos(){
    this._eventosService.getEventos().subscribe(events => {
      this.eventos = events;
    })
    for(var x in this.eventos){
      console.log('Ejemplo');
    }
  }*/

  async getEventos(){
      let eventdata = {}
      eventdata= await this._eventosService.getEventos().toPromise()
      for(let i=0; i<1; i++){
        console.log(eventdata[i]['title']);
      }
      this.titleEvent= eventdata[0]['title'];
  }

}
