import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {

   @Input() titulo: string =''; // todo input é naturalmente um public
  constructor() { }

  ngOnInit() {
  }

}
