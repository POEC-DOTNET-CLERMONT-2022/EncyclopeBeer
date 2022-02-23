import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { AppRoutingModule } from './app-routing.module';
import { AppAuthenticationModule } from './app-authentification.module';
import { AuthHttpInterceptor } from '@auth0/auth0-angular';

import { AppComponent } from './app.component';
import { ListBeersComponent } from './components/listbeers/listbeers.component';
import { BeerDetailsComponent } from './components/beerdetails/beerdetails.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginButtonComponent } from './components/login-button/login-button.component';
import { SignupButtonComponent } from './components/signup-button/signup-button.component';
import { LogoutButtonComponent } from './components/logout-button/logout-button.component';
import { AuthenticationButtonComponent } from './components/authentication-button/authentication-button.component';
import { AuthNavComponent } from './components/auth-nav/auth-nav.component';


@NgModule({
  declarations: [
    AppComponent,
    ListBeersComponent,
    BeerDetailsComponent,
    NavbarComponent,
    LoginButtonComponent,
    SignupButtonComponent,
    LogoutButtonComponent,
    AuthenticationButtonComponent,
    AuthNavComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AppAuthenticationModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS, useClass: AuthHttpInterceptor, multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
