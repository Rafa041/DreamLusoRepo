import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./Pages/home/home.component";
import { CreateUserComponent } from "./Pages/create-user/create-user.component";
import { LoginComponent } from "./Pages/login/login.component";
import { RetrieveAllPropertiesComponent } from "./Pages/retrieve-all-properties/retrieve-all-properties.component";
import { RetrievePropertyIdComponent } from "./Pages/retrieve-property-id/retrieve-property-id.component";

import { PropertiesServicesComponent } from "./Pages/properties-services/properties-services.component";
import { PropertiesRentComponent } from "./Pages/properties-rent/properties-rent.component";
import { PropertiesAlgarveComponent } from "./Pages/properties-algarve/properties-algarve.component";
import { PropertiesPurchaseComponent } from "./Pages/properties-purchase/properties-purchase.component";
import { PropertiesPortoComponent } from "./Pages/properties-porto/properties-porto.component";
import { PropertiesLisbonComponent } from "./Pages/properties-lisbon/properties-lisbon.component";


const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    children: [],
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: CreateUserComponent },
  { path: 'properties', component: RetrieveAllPropertiesComponent },
  { path: 'propertyId/:id', component: RetrievePropertyIdComponent },
  { path: 'propertiesBuy', component: PropertiesPurchaseComponent },
  { path: 'propertiesRent', component: PropertiesRentComponent },
  { path: 'propertiesPorto', component: PropertiesPortoComponent },
  { path: 'propertiesLisbon', component: PropertiesLisbonComponent },
  { path: 'propertiesAlgarve', component: PropertiesAlgarveComponent },
  //{ path: 'propertiesServices', component: PropertiesServicesComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class FrontOfficeRoutingModule {}



