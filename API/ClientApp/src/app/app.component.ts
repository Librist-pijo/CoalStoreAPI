import { Component } from '@angular/core';
import { locale, loadMessages, formatMessage } from 'devextreme/localization';
import * as plMessages from '../assets/pl.json';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title: string = "ClientApp";

  constructor(
    private authService: AuthService
  ) {
    this.initMessages();
    locale("pl");
    this.authService.checkIfTokenIsValid();
  }

  
  initMessages() {
    loadMessages(plMessages);
  }

}
