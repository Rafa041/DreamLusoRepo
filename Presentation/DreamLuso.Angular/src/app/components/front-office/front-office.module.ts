import { MainComponent } from './main.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared.module';
import { FrontOfficeRoutingModule } from './front-office-routing.module';
import { HeaderComponent } from './layout/header/header.component';
import { UserComponent } from './user/user.component';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { HttpClientModule, HttpClientXsrfModule } from '@angular/common/http';
@NgModule({
  declarations: [
    MainComponent,
    HeaderComponent,
    UserComponent,
    LoginComponent,
    RegisterUserComponent
  ],
  imports: [FrontOfficeRoutingModule, CommonModule, SharedModule, FormsModule, HttpClientModule],
})
export class FrontOfficeModule {}
