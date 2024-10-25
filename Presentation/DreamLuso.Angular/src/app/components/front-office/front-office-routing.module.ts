import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { UserComponent } from './Pages/user/user.component';
import { LoginComponent } from './Pages/login/login.component';
import { RegisterUserComponent } from './Pages/register-user/register-user.component';
import { DashboardComponent } from './Pages/dashboard/dashboard.component';
import { CreateAgentComponent } from '../back-office/Pages/create-agent/create-agent.component';

const routes: Routes = [
  {
    path: '',
    component: HeaderComponent,
    children: [
      { path: 'user', component: UserComponent },
      { path: 'user/:id', component: UserComponent },

    ],
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterUserComponent },
  { path: 'dash', component: DashboardComponent },
  { path: 'createAgent', component: CreateAgentComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class FrontOfficeRoutingModule {}



