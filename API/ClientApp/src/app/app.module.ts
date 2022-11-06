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
import { DxButtonModule, DxDataGridModule, DxDateBoxModule, DxTabsModule, DxTextBoxModule, DxValidatorModule } from 'devextreme-angular';
import { EmailValidationService } from './services/email-validation.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { UserAccountComponent } from './layout/user-account/user-account.component';
import { ProductsComponent } from './layout/products/products.component';
import { OrdersService } from './services/orders.service';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    FooterComponent,
    HomeComponent,
    LoginComponent,
    UserAccountComponent,
    ProductsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    DxButtonModule,
    DxTextBoxModule,
    DxDateBoxModule,
    DxValidatorModule,
    DxTabsModule,
    DxDataGridModule
  ],
  providers: [
    Connection, 
    DatabaseService,
    EmailValidationService,
    OrdersService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
