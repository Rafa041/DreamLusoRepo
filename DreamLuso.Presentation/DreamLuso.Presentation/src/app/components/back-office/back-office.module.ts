import { CommonModule } from "@angular/common";
import { BackOfficeRoutingModule } from "./back-office-routing.module";
import { SharedModule } from "../../shared.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgModule } from "@angular/core";
import { AddPropertyComponent } from './Pages/Agent/Pages/add-property/add-property.component';
import { imageUploadComponent } from './Components/image-upload/image-upload.component';
import { DashboardAdminComponent } from './Pages/Admin/Pages/dashboard/dashboard.component';
import { CreateAgentComponent } from './Pages/Admin/Pages/create-agent/create-agent.component';
import { DashboardAgentComponent } from "./Pages/Agent/Pages/dashboard/dashboard.component";
import { DashboardClientComponent } from "./Pages/Client/Pages/dashboard/dashboard.component";
import { DashboardSidebarAgentComponent } from './Pages/Agent/Components/dashboard-sidebar-agent/dashboard-sidebar-agent.component';
import { RealStateAgentPropertiesComponent } from './Pages/Agent/Pages/real-state-agent-properties/real-state-agent-properties.component';
import { Router, RouterModule } from "@angular/router";
import { UpdatePropertyComponent } from './Pages/Agent/Pages/update-property/update-property.component';
import { AppointmentsComponent } from './Pages/Agent/Pages/appointments/appointments.component';
import { AppointmentCalendarComponent } from './Pages/Agent/Components/appointment-calendar/appointment-calendar.component';
import { ListOfContractAgentComponent } from './Pages/Agent/Pages/list-of-contract-agent/list-of-contract-agent.component';
import { DashboardSidebarClientComponent } from './Pages/Client/Components/dashboard-sidebar-client/dashboard-sidebar-client.component';
import { DashboardSidebarAdminComponent } from './Pages/Admin/Components/dashboard-sidebar-admin/dashboard-sidebar-admin.component';
import { UsersComponent } from './Pages/Admin/Pages/users/users.component';
import { ChatOverlayComponent } from './Pages/Agent/Pages/chat-overlay/chat-overlay.component';
import { ChatClientComponent } from './Pages/Client/Pages/chat-client/chat-client.component';
//import { ChatAgentComponent } from './Pages/Agent/Pages/chat-agent/chat-agent.component';


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
    AppointmentsComponent,
    AppointmentCalendarComponent,
    ListOfContractAgentComponent,
    DashboardSidebarClientComponent,
    DashboardSidebarAdminComponent,
    UsersComponent,
    ChatOverlayComponent,
    ChatClientComponent,
    //ChatAgentComponent,
  ]
})
export class BackOfficeModule { }
