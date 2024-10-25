import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { GetAllUsersComponent } from './Pages/get-all-users/get-all-users.component';
import { CreateAgentComponent } from './Pages/create-agent/create-agent.component';
import { AddPropertyComponent } from './Pages/add-property/add-property.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,

  },

    { path: 'dashboard', component: DashboardComponent },
    { path: 'createAgent', component: CreateAgentComponent }, // por testar
    { path: 'users', component: GetAllUsersComponent }, // por testar
    { path: 'property', component: AddPropertyComponent }, // por testar

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BackOfficeRoutingModule {}
