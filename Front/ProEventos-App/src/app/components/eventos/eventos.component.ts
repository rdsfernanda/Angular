import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
//import { Evento } from '../models/Evento';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/models/services/evento.service'; // referencia configurada no paths em tsconfig.json


@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  ngOnInit(): void {

  }
}
