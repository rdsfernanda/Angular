import { listLazyRoutes } from '@angular/compiler/src/aot/lazy_routes';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {

   @Input() titulo: string ='';
   @Input() subtitulo: string ='Desde 2021';
   @Input() iconClass: string ='fa fa-user';
   @Input() botaoListar=false; // todo input é naturalmente um public
  constructor(private router: Router) { } //injetando rota

  ngOnInit() {}
  listar():void{
    this.router.navigate([`/${this.titulo.toLocaleLowerCase()}/lista`])
  }

}