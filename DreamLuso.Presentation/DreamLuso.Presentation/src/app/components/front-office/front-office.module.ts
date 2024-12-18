import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SharedModule } from "../../shared.module";
import { CommonModule } from "@angular/common";
import { FrontOfficeRoutingModule } from "./front-office-routing.module";
import { NgModule } from "@angular/core";
import { HomeComponent } from "./Pages/home/home.component";
import { LoginComponent } from './Pages/login/login.component';
import { CreateUserComponent } from './Pages/create-user/create-user.component';
import { RetrieveAllPropertiesComponent } from './Pages/retrieve-all-properties/retrieve-all-properties.component';
import { RetrievePropertyIdComponent } from './Pages/retrieve-property-id/retrieve-property-id.component';
import { ScheduleVisitComponent } from './Components/schedule-visit/schedule-visit.component';

import { PropertiesServicesComponent } from './Pages/properties-services/properties-services.component';
import { PropertiesRentComponent } from "./Pages/properties-rent/properties-rent.component";
import { PropertiesAlgarveComponent } from "./Pages/properties-algarve/properties-algarve.component";
import { PropertiesPurchaseComponent } from "./Pages/properties-purchase/properties-purchase.component";
import { PropertiesPortoComponent } from "./Pages/properties-porto/properties-porto.component";
import { PropertiesLisbonComponent } from "./Pages/properties-lisbon/properties-lisbon.component";


@NgModule({
  declarations: [
    //Declare aqui os components

    HomeComponent,
    LoginComponent,
    CreateUserComponent,
    RetrieveAllPropertiesComponent,
    RetrievePropertyIdComponent,
    ScheduleVisitComponent,
    PropertiesPurchaseComponent,
    PropertiesRentComponent,
    PropertiesPortoComponent,
    PropertiesAlgarveComponent,
    PropertiesLisbonComponent,
    //PropertiesServicesComponent,
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
