import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminServicesService {
  readonly apiUrl = 'http://localhost:5016/api';

  constructor(private http: HttpClient) { }

  // Games
  getProductList(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + '/Product/all');
  }

  addProduct(game: any): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<any>(this.apiUrl + '/Product', game, httpOptions);
  }

  updateProduct(game: any): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<any>(this.apiUrl + '/Product/update/:id', game, httpOptions);
  }

  deleteProduct(gameId: number): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.delete<number>(this.apiUrl + '/Product/delete/:id' + gameId, httpOptions);
  }

  // Member Page
  getUserList(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + '/User/all');
  }

  updateUser(userId: number): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<any>(this.apiUrl + '/User/:id', userId, httpOptions);
  }

  deleteUser(userId: number): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.delete<number>(this.apiUrl + '/User/:id' + userId, httpOptions);
  }

  // Dashboard
  getNumberOfMembers(): Observable<any> {
    return this.http.get<any>(this.apiUrl + '/User/count');
  }

  getNumberOfSales(): Observable<any> {
    return this.http.get<any>(this.apiUrl + '/Sale/count');
  }

  getNumberOfSalesLast7Days(): Observable<any> {
    return this.http.get<any>(this.apiUrl + '/Sale/count/7');
  }

  getTotalSiteRevenues(): Observable<any> {
    return this.http.get<any>(this.apiUrl + '/Sale/revenue');
  }

  getSiteRevenuesLast7Days(): Observable<any> {
    return this.http.get<any>(this.apiUrl + '/Sale/revenue/7');
  }

  // Photo
  uploadPhoto(photo: any) {
    return this.http.post(this.apiUrl + '/User/:id', photo);
  }
}