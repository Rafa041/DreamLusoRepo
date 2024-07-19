import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared.module';
import { BackOfficeRoutingModule } from './back-office-routing.module';
import { HeaderComponent } from './layout/header/header.component';
import { MainComponent } from './main.component';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
  imports: [
    BackOfficeRoutingModule,
    CommonModule,
    SharedModule,
  ],
  declarations: [HeaderComponent, MainComponent, DashboardComponent]
})
export class BackOfficeModule { }
