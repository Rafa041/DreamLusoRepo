import { MainComponent } from './main.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared.module';
import { FrontOfficeRoutingModule } from './front-office-routing.module';
import { HeaderComponent } from './layout/header/header.component';
import { UserComponent } from './Pages/user/user.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './Pages/login/login.component';
import { RegisterUserComponent } from './Pages/register-user/register-user.component';
import { HttpClientModule } from '@angular/common/http';
import { DashboardComponent } from './Pages/dashboard/dashboard.component';

@NgModule({
  declarations: [
    MainComponent,
    HeaderComponent,
    UserComponent,
    LoginComponent,
    RegisterUserComponent,
    DashboardComponent
  ],
  imports: [
    FrontOfficeRoutingModule,
    CommonModule,
    SharedModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
})
export class FrontOfficeModule {}
