import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CreatePropertyComponent } from './Pages/create-property/create-property.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,

  },

    { path: 'dashboard', component: DashboardComponent },
    { path: 'create', component: CreatePropertyComponent },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BackOfficeRoutingModule {}
