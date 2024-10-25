import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SharedModule } from "../../shared.module";
import { CommonModule } from "@angular/common";
import { FrontOfficeRoutingModule } from "./front-office-routing.module";
import { NgModule } from "@angular/core";
import { HomeComponent } from "./Pages/home/home.component";
import { LoginComponent } from './Pages/login/login.component';
import { CreateUserComponent } from './Pages/create-user/create-user.component';
import { DashboardComponent } from './Pages/dashboard/dashboard.component';
import { RetrieveAllPropertiesComponent } from './Pages/retrieve-all-properties/retrieve-all-properties.component';
import { RetrievePropertyIdComponent } from './Pages/retrieve-property-id/retrieve-property-id.component';




@NgModule({
  declarations: [
    //Declare aqui os components

    HomeComponent,
    LoginComponent,
    CreateUserComponent,
    DashboardComponent,
    RetrieveAllPropertiesComponent,
    RetrievePropertyIdComponent,
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
