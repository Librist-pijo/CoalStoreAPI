import { Injectable } from "@angular/core";
import { Connection } from "../models/Connection";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class DatabaseService {
  connection: Connection;
  route: string;
  json = 'json/';

  constructor(private http: HttpClient) {
    this.connection = new Connection();
    this.route = "";
  }

  SetRoute(_path: string) {
    this.route = 'api/' + _path;
  }

  Add<T>(_obj: any): Observable<T> {
    return this.http.post<T>(this.route, _obj);
  }

  Get<T>(): Observable<T> {
    return this.http.get<T>(this.route, {
      responseType: 'json'
    });
  }

  Update<T>(_obj: any): Observable<T> {
    return this.http.put<T>(this.route, _obj);
  }

  Delete<T>(): Observable<any> {
    return this.http.delete<T>(this.route);
  }
}
