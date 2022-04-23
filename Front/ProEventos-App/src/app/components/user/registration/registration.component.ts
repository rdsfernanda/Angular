import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorsField } from '@app/helpers/ValidatorsField';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {


  form!: FormGroup;

  get f(): any{
    return this.form.controls; // chama os controls do form group
  }
  constructor(public fb: FormBuilder) { }

  ngOnInit(): void {
    this.validation();
  }

  private validation(): void{

    const formOptions: AbstractControlOptions = {
      validators: ValidatorsField.MustMatch('senha','confirmeSenha')
    };

    this.form= this.fb.group(
      {
        primeiroNome:['',
        [Validators.required,Validators.minLength(4), Validators.maxLength(250)]
      ]  ,
        ultimoNome: ['',Validators.required]   ,
        email: ['',
        [Validators.required,Validators.email]
      ]  ,
        userName:['',Validators.required]   ,
        senha: ['',
        [Validators.required, Validators.minLength(6)]
      ]  ,
        confirmeSenha:['',Validators.required],

      }, formOptions);
  }

}
