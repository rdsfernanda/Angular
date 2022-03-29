import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContatosComponent } from './components/contatos/contatos.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { EventosComponent } from './components/eventos/eventos.component';
import { PalestrantesComponent } from './components/palestrantes/palestrantes.component';
import { PerfilComponent } from './components/perfil/perfil.component';

const routes: Routes = [
  {path: 'eventos', component:EventosComponent},
  {path: 'dashboard', component:DashboardComponent},
  {path: 'palestrantes', component:PalestrantesComponent},
  {path: 'perfil', component:PerfilComponent},
  {path: 'contatos', component:ContatosComponent},
  {path: '', redirectTo: 'dashboard', pathMatch:'full'}, // se for vazio direciona para dashboard
  {path: '**', redirectTo: 'dashboard', pathMatch:'full'} // ** = qualquer coisa na URL direciona para dashboard novamente
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
