import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AddPropertyComponent } from "./Pages/add-property/add-property.component";
import { HomeComponent } from "../front-office/Pages/home/home.component";
import { DashboardComponent } from "./Pages/dashboard/dashboard.component";
import { CreateAgentComponent } from "./Pages/create-agent/create-agent.component";



const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
    children: [
    ]
  },
  { path: 'property',component: AddPropertyComponent },
  { path: 'dashboard',component: DashboardComponent },
  { path: 'createAgent',component: CreateAgentComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BackOfficeRoutingModule {}
