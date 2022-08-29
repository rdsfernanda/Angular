import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../Evento';

@Injectable(
  //{providedIn: 'root'}
)
export class EventoService {

  baseURL='https://localhost:5001/api/eventos';
constructor(private http: HttpClient) { }


    getEventos(): Observable<Evento[]>{
      return this.http.get<Evento[]>(this.baseURL);
    }

    getEventosByTema(tema:string): Observable<Evento[]>{
      return this.http.get<Evento[]>(`${this.baseURL}/${tema}/tema`);
    }

    getEventoById(id:number): Observable<Evento>{
      return this.http.get<Evento>(`${this.baseURL}/${id}`);
    }
    post(evento:Evento): Observable<Evento>{
      return this.http.post<Evento>(this.baseURL,evento);
    }

    put( evento: Evento): Observable<Evento>{
      return this.http.put<Evento>(`${this.baseURL}/${evento.id}`,evento);
    }

    deleteEvento(id:number): Observable<any>{
      return this.http.delete(`${this.baseURL}/${id}`);
    }

}
