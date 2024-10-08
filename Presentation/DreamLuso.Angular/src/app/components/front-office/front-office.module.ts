import { MainComponent } from './main.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared.module';
import { FrontOfficeRoutingModule } from './front-office-routing.module';
import { HeaderComponent } from './layout/header/header.component';
import { UserComponent } from './user/user.component';
import { UserListComponent } from './user-list/user-list.component';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
@NgModule({
  declarations: [
    MainComponent,
    HeaderComponent,
    UserComponent,
    UserListComponent,
    LoginComponent,
  ],
  imports: [FrontOfficeRoutingModule, CommonModule, SharedModule, FormsModule],
})
export class FrontOfficeModule {}
