import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  form!: FormGroup;

  get f(): any{
    return this.form.controls; // chama os controls do form group
  }
  constructor() { }

  ngOnInit(): void {
  }

}
