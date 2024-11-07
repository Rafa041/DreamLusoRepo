import { CommonModule } from "@angular/common";
import { BackOfficeRoutingModule } from "./back-office-routing.module";
import { SharedModule } from "../../shared.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgModule } from "@angular/core";
import { AddPropertyComponent } from './Pages/Agent/Pages/add-property/add-property.component';
import { imageUploadComponent } from './Components/image-upload/image-upload.component';
import { DashboardAdminComponent } from './Pages/Admin/dashboard/dashboard.component';
import { CreateAgentComponent } from './Pages/Admin/create-agent/create-agent.component';
import { DashboardAgentComponent } from "./Pages/Agent/Pages/dashboard/dashboard.component";
import { DashboardClientComponent } from "./Pages/Client/dashboard/dashboard.component";
import { DashboardSidebarAgentComponent } from './Pages/Agent/Components/dashboard-sidebar-agent/dashboard-sidebar-agent.component';
import { RealStateAgentPropertiesComponent } from './Pages/Agent/Pages/real-state-agent-properties/real-state-agent-properties.component';
import { Router, RouterModule } from "@angular/router";
import { UpdatePropertyComponent } from './Pages/Agent/Pages/update-property/update-property.component';


@NgModule({
  imports: [
    BackOfficeRoutingModule,
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
  ],
  declarations: [
   // Certifique-se de declarar seus componentes aqui
    AddPropertyComponent,
    imageUploadComponent,
    DashboardAdminComponent,
    CreateAgentComponent,
    DashboardAgentComponent,
    DashboardClientComponent,
    DashboardSidebarAgentComponent,
    RealStateAgentPropertiesComponent,
    UpdatePropertyComponent,
  ]
})
export class BackOfficeModule { }
