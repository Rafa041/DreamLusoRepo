import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AddPropertyComponent } from "./Pages/Agent/Pages/add-property/add-property.component";
import { HomeComponent } from "../front-office/Pages/home/home.component";
import { DashboardAdminComponent } from "./Pages/Admin/dashboard/dashboard.component";
import { CreateAgentComponent } from "./Pages/Admin/create-agent/create-agent.component";
import { DashboardAgentComponent } from "./Pages/Agent/Pages/dashboard/dashboard.component";
import { DashboardClientComponent } from "./Pages/Client/dashboard/dashboard.component";
import { RealStateAgentPropertiesComponent } from "./Pages/Agent/Pages/real-state-agent-properties/real-state-agent-properties.component";
import { UpdatePropertyComponent } from "./Pages/Agent/Pages/update-property/update-property.component";



const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
    children: [
    ]
  },
  { path: 'agent/createProperty',component: AddPropertyComponent },
  { path: 'createAgent',component: CreateAgentComponent },
  { path: 'dashboardAgent',component: DashboardAgentComponent },
  { path: 'dashboardClient',component: DashboardClientComponent },
  { path: 'dashboardAdmin',component: DashboardAdminComponent },
  { path: 'dashboardAgent',component: DashboardAgentComponent },
  { path: 'agent/properties',component: RealStateAgentPropertiesComponent },
  { path: 'agent/properties/edit/:id', component: UpdatePropertyComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BackOfficeRoutingModule {}
