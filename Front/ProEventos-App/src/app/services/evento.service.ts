import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '@app/models/Evento';
import {take} from 'rxjs/operators';

@Injectable(
  //{providedIn: 'root'}
)
export class EventoService {

  baseURL='https://localhost:5001/api/eventos';
constructor(private http: HttpClient) { }


    getEventos(): Observable<Evento[]>{
      return this.http
      .get<Evento[]>(this.baseURL)
      .pipe(take(1));//desinscrever
    }

    getEventosByTema(tema:string): Observable<Evento[]>{
      return this.http
      .get<Evento[]>(`${this.baseURL}/${tema}/tema`)
      .pipe(take(1));
    }

    getEventoById(id:number): Observable<Evento>{
      return this.http
      .get<Evento>(`${this.baseURL}/${id}`)
      .pipe(take(1));
    }
    post(evento:Evento): Observable<Evento>{
      return this.http
      .post<Evento>(this.baseURL,evento)
      .pipe(take(1));
    }

    put( evento: Evento): Observable<Evento>{
      return this.http
      .put<Evento>(`${this.baseURL}/${evento.id}`,evento)
      .pipe(take(1));
    }

    deleteEvento(id:number): Observable<any>{
      return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
    }

}
