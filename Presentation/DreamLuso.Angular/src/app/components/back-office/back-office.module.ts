import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared.module';
import { BackOfficeRoutingModule } from './back-office-routing.module';
import { HeaderComponent } from './layout/header/header.component';
import { MainComponent } from './main.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { GetAllUsersComponent } from './Pages/get-all-users/get-all-users.component';
import { CreateAgentComponent } from './Pages/create-agent/create-agent.component';
import { AddPropertyComponent } from './Pages/add-property/add-property.component';


@NgModule({
  imports: [
    BackOfficeRoutingModule,
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  declarations: [
    HeaderComponent, // Certifique-se de declarar seus componentes aqui
    MainComponent,
    DashboardComponent,
    GetAllUsersComponent,
    CreateAgentComponent,
    AddPropertyComponent,
  ]
})
export class BackOfficeModule { }
