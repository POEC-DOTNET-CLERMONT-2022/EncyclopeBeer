import { NgModule } from '@angular/core';
import { AuthModule } from '@auth0/auth0-angular';
import { environment as env } from '../environments/environment';

@NgModule({
  imports: [
    AuthModule.forRoot({
      ...env.auth,
    }),
  ],
  exports: [AuthModule],
  declarations: [],
  providers: [],
})

export class AppAuthenticationModule { }
