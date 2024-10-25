import { CommonModule } from "@angular/common";
import { BackOfficeRoutingModule } from "./back-office-routing.module";
import { SharedModule } from "../../shared.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgModule } from "@angular/core";
import { AddPropertyComponent } from './Pages/add-property/add-property.component';
import { imageUploadComponent } from './Components/image-upload/image-upload.component';
import { DashboardComponent } from './Pages/dashboard/dashboard.component';
import { CreateAgentComponent } from './Pages/create-agent/create-agent.component';


@NgModule({
  imports: [
    BackOfficeRoutingModule,
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  declarations: [
   // Certifique-se de declarar seus componentes aqui
    AddPropertyComponent,
    imageUploadComponent,
    DashboardComponent,
    CreateAgentComponent,

  ]
})
export class BackOfficeModule { }
