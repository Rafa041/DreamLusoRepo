import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, provideHttpClient, withFetch } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './shared.module';
import { UserService } from './services/UserService/user.service';
import { AuthService } from './services/AuthService/auth.service';
import { PropertyService } from './services/PropertyService/property.service';
import { RealStateAgentService } from './services/RealStateAgent/real-state-agent.service';
import { ErrorComponent } from './components/common/error/error.component';

@NgModule({
  declarations: [
    AppComponent,
    ErrorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    SharedModule,
  ],
  providers: [
    provideClientHydration(),
    provideHttpClient(withFetch()),
    UserService,
    AuthService,
    PropertyService,
    RealStateAgentService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
