import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { HttpClientModule, HttpClientXsrfModule, provideHttpClient, withFetch } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared.module';
import { ErrorComponent } from './components/common/error/error.component';
import { UserService } from './services/user.service';
import { FormsModule } from '@angular/forms';
import { AuthService } from './services/auth.service';
import { PropertyService } from './services/property.service';
import { ReactiveFormsModule } from '@angular/forms';
import { RealStateAgentService } from './services/realStateAgent.service';
@NgModule({
  declarations: [
    AppComponent,
    ErrorComponent,

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
