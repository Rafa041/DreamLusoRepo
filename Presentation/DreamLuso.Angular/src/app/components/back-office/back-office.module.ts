import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared.module';
import { BackOfficeRoutingModule } from './back-office-routing.module';
import { HeaderComponent } from './layout/header/header.component';
import { MainComponent } from './main.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CreatePropertyComponent } from './Pages/create-property/create-property.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    BackOfficeRoutingModule,
    CommonModule,
    SharedModule,
    FormsModule,
    HttpClientModule,
  ],
  declarations: [
      HeaderComponent,
      MainComponent,
      DashboardComponent,
      CreatePropertyComponent,
    ]
})
export class BackOfficeModule { }
