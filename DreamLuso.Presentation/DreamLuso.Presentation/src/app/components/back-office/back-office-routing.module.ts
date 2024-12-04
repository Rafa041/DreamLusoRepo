import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AddPropertyComponent } from "./Pages/Agent/Pages/add-property/add-property.component";
import { HomeComponent } from "../front-office/Pages/home/home.component";
import { DashboardAdminComponent } from "./Pages/Admin/Pages/dashboard/dashboard.component";
import { CreateAgentComponent } from "./Pages/Admin/Pages/create-agent/create-agent.component";
import { DashboardAgentComponent } from "./Pages/Agent/Pages/dashboard/dashboard.component";
import { DashboardClientComponent } from "./Pages/Client/Pages/dashboard/dashboard.component";
import { RealStateAgentPropertiesComponent } from "./Pages/Agent/Pages/real-state-agent-properties/real-state-agent-properties.component";
import { UpdatePropertyComponent } from "./Pages/Agent/Pages/update-property/update-property.component";
import { AppointmentsComponent } from "./Pages/Agent/Pages/appointments/appointments.component";
import { UsersComponent } from "./Pages/Admin/Pages/users/users.component";
import { ChatOverlayComponent } from "./Pages/Agent/Pages/chat-overlay/chat-overlay.component";
import { ChatClientComponent } from "./Pages/Client/Pages/chat-client/chat-client.component";
import { ChatAgentComponent } from "./Pages/Agent/Pages/chat-agent/chat-agent.component";



const routes: Routes = [
  {
    path: 'admin',
    children: [
      { path: 'dashboard', component: DashboardAdminComponent },
      { path: 'users', component: UsersComponent },
      { path: 'create-agent', component: CreateAgentComponent }
    ]
  },
  {
    path: 'agent',
    children: [
      { path: 'dashboard', component: DashboardAgentComponent },
      { path: 'create-property', component: AddPropertyComponent },
      { path: 'properties', component: RealStateAgentPropertiesComponent },
      { path: 'properties/edit/:id', component: UpdatePropertyComponent },
      { path: 'appointments', component: AppointmentsComponent },
      {path: 'chat', component: ChatAgentComponent}
    ]
  },
  {
    path: 'client',
    children: [
      { path: 'dashboard', component: DashboardClientComponent },
      { path: 'chat-overlay', component: ChatOverlayComponent },
      { path: 'chatClient', component: ChatClientComponent }
    ]
  },
  {
    path: '',
    component: HomeComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BackOfficeRoutingModule {}
