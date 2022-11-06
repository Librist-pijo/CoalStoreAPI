import { Component } from '@angular/core';
import { locale, loadMessages, formatMessage } from 'devextreme/localization';
import * as plMessages from '../assets/pl.json';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title: string = "ClientApp";

  constructor() {
    this.initMessages();
    locale("pl");
  }

  
  initMessages() {
    loadMessages(plMessages);
  }

}
