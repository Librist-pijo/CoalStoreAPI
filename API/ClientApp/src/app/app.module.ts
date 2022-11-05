import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { Connection } from './models/Connection';
import { DatabaseService } from './services/database.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './layout/nav/nav.component';
import { HomeComponent } from './layout/home/home.component';
import { FooterComponent } from './layout/footer/footer.component';
import { LoginComponent } from './layout/login/login.component';
import { DxButtonModule, DxDateBoxModule, DxTextBoxModule, DxValidatorModule } from 'devextreme-angular';
import { EmailValidationService } from './services/email-validation.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    FooterComponent,
    HomeComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    DxButtonModule,
    DxTextBoxModule,
    DxDateBoxModule,
    DxValidatorModule
  ],
  providers: [
    Connection, 
    DatabaseService,
    EmailValidationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
