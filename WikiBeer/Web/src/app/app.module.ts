import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { AppRoutingModule } from './app-routing.module';
import { AppAuthenticationModule } from './app-authentification.module';
import { AuthHttpInterceptor } from '@auth0/auth0-angular';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { CommonModule } from '@angular/common';

// Material design
import { MatCardModule} from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatIconModule } from '@angular/material/icon';

// Component
import { BeerCardComponent } from './components/beer-card/beer-card.component'
import { AppComponent } from './app.component';
import { ListBeersComponent } from './components/listbeers/listbeers.component';
import { BeerDetailsComponent } from './components/beerdetails/beerdetails.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginButtonComponent } from './components/login-button/login-button.component';
import { SignupButtonComponent } from './components/signup-button/signup-button.component';
import { LogoutButtonComponent } from './components/logout-button/logout-button.component';
import { AuthenticationButtonComponent } from './components/authentication-button/authentication-button.component';
import { AuthNavComponent } from './components/auth-nav/auth-nav.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { UserService } from 'src/services/user.service';
import { BeerService } from 'src/services/beer.service';
import { StarComponent } from './components/star/star.component';
import { CountryService } from 'src/services/country.service';

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
    AuthNavComponent,
    UserProfileComponent,
    BeerCardComponent,
    StarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AppAuthenticationModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatToolbarModule,
    MatButtonModule,
    FlexLayoutModule,
    MatIconModule,
    FormsModule,
    Ng2SearchPipeModule,
    CommonModule,
    ReactiveFormsModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS, useClass: AuthHttpInterceptor, multi: true
  }, UserService, BeerService, CountryService],
  bootstrap: [AppComponent]
})

export class AppModule { }
