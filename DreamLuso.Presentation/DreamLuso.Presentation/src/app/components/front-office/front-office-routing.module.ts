import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./Pages/home/home.component";
import { CreateUserComponent } from "./Pages/create-user/create-user.component";
import { LoginComponent } from "./Pages/login/login.component";
import { RetrieveAllPropertiesComponent } from "./Pages/retrieve-all-properties/retrieve-all-properties.component";
import { RetrievePropertyIdComponent } from "./Pages/retrieve-property-id/retrieve-property-id.component";


const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    children: [
      //{ path: 'user', component: UserComponent },
      //{ path: 'user/:id', component: UserComponent },

    ],
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: CreateUserComponent },
  { path: 'properties', component: RetrieveAllPropertiesComponent },
  { path: 'propertyId/:id', component: RetrievePropertyIdComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class FrontOfficeRoutingModule {}



