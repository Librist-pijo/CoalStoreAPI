import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
import Swal from 'sweetalert2';
import { GrantType } from '../models/Enums/GrantType';
import { LoginDTO } from '../models/LoginDTO';
import { ResultData } from '../models/ResultData';
import { OrdersService } from './orders.service';
import { TokenService } from './token.service';

const OAUTH_CLIENT = 'express-client';
const OAUTH_SECRET = 'express-secret';
const API_URL = 'https://localhost:7197/api/';
const LOGIN_KEY = 'user_loggin';

const HTTP_OPTIONS = {
  headers: new HttpHeaders({
    'Content-Type': 'application/x-www-form-urlencoded',
    Authorization: 'Basic ' + btoa(OAUTH_CLIENT + ':' + OAUTH_SECRET)
  })
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  redirectUrl = '';

  constructor(
    private http: HttpClient, 
    private tokenService: TokenService,
    private ordersService: OrdersService
  ) { }

  private static handleError(error: HttpErrorResponse): any {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    return throwError(
      'Something bad happened; please try again later.');
  }

  private static log(message: string): any {
    console.log(message);
  }

  login(loginData: LoginDTO): Observable<any> {
    this.tokenService.removeToken();
    this.tokenService.removeRefreshToken();

    loginData.grantType = GrantType.Password;

    return this.http.post<ResultData<LoginDTO>>(API_URL + 'auth/login', loginData)//, HTTP_OPTIONS)
      .pipe(
        tap(res => {
          if (res.success && res.data) {
            if (res.data.accessToken) {
              this.tokenService.saveToken(res.data.accessToken);
            }
            if (res.data.refreshToken) {
              this.tokenService.saveRefreshToken(res.data.refreshToken);
            }
            this.saveLoggedUserLogin(res.data.login);
          } 
        }),
        catchError(AuthService.handleError)
      );
  }

  refreshToken(refreshData: any): Observable<any> {
    this.tokenService.removeToken();
    this.tokenService.removeRefreshToken();
    const body = new HttpParams()
      .set('refresh_token', refreshData.refresh_token)
      .set('grant_type', GrantType.RefreshToken);
    return this.http.post<any>(API_URL + 'auth/refreshtoken', body, HTTP_OPTIONS)
      .pipe(
        tap(res => {
          this.tokenService.saveToken(res.access_token);
          this.tokenService.saveRefreshToken(res.refresh_token);
          this.saveLoggedUserLogin(res.login);
        }),
        catchError(AuthService.handleError)
      );
  }

  logout(): void {
    this.ordersService.resetShoppingCart();
    this.tokenService.removeToken();
    this.tokenService.removeRefreshToken();
    this.removeLoggerUserLogin();
  }

  register(data: any): Observable<any> {
    return this.http.post<any>(API_URL + 'auth/register', data)
      .pipe(
        tap(_ => AuthService.log('register')),
        catchError(AuthService.handleError)
      );
  }

  saveLoggedUserLogin(login: string) {
    localStorage.setItem(LOGIN_KEY, login);
  }

  removeLoggerUserLogin() {
    localStorage.removeItem(LOGIN_KEY);
  }

  getLoggedUserLogin(): string {
    return localStorage.getItem(LOGIN_KEY) ?? '';
  }

  checkIfTokenIsValid() {
    var token = this.tokenService.getToken();

    if (token) {
      const body = new HttpParams()
        .set('token', token);
      this.http.get<boolean>(API_URL + 'auth/gettokenvalidation', { params: body }).toPromise().then((data) => {
        if (data !== true) {
          this.logout();
        } 
      }, (err) => {
        this.logout();
      });
    }
  }
}
