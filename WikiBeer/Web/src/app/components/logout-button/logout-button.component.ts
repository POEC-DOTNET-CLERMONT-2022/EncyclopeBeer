import { Component, Inject, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-logout-button',
  templateUrl: './logout-button.component.html',
  styleUrls: ['./logout-button.component.scss'],
})
export class LogoutButtonComponent implements OnInit {

  private _doc: Document;
  private _auth: AuthService;

  constructor(
    auth: AuthService,
    @Inject(DOCUMENT) doc: Document,
  ) {
    this._auth = auth;
    this._doc = doc;
  }

  ngOnInit(): void {}

  logout(): void {
    this._auth.logout({ returnTo: this._doc.location.origin });
  }
}
