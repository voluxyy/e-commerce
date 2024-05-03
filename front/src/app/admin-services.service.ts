import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminServicesService {
  readonly apiUrl = 'http://localhost:5016';

  constructor(private http: HttpClient) { }

  // Games
  getProductList(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + '/api/Product/all');
  }

  addProduct(game: any): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<any>(this.apiUrl + '/api/Product', game, httpOptions);
  }

  updateProduct(game: any): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<any>(this.apiUrl + '/api/Product/update/:id', game, httpOptions);
  }

  deleteProduct(gameId: number): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.delete<number>(this.apiUrl + '/api/Product/delete/:id' + gameId, httpOptions);
  }

  // Member Page
  getUserList(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + '/api/User/all');
  }

  updateUser(user: any): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<any>(this.apiUrl + 'employee/UpdateEmployee/', user, httpOptions);
  }

  deleteUser(userId: number): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.delete<number>(this.apiUrl + 'employee/DeleteEmployee/' + userId, httpOptions);
  }

  // Dashboard
  // get number of members
  // get number of sales
  // get number of the sales on the last 7 days
  // total site revenues
  // site revenues on the last 7 days

  // Photo
  uploadPhoto(photo: any) {
    return this.http.post(this.apiUrl + '/api/User/:id', photo);
  }
}