import { NgFor, NgIf } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-games-admin',
  standalone: true,
  imports: [RouterOutlet, NgFor, NgIf],
  templateUrl: './games-admin.component.html',
  styleUrl: './games-admin.component.scss'
})
export class GamesAdminComponent {
  products: any;
  prod: any;
  apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = 'http://localhost:5016/api/Product';
  }

  addGame(prod: any): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<any>(this.apiUrl + "/add", prod, httpOptions);
  }

  editGame(prod: any): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<any>(this.apiUrl + "/update", prod, httpOptions);
  }

  // deleteGame(productId: number): Observable<any> {
  //   const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
  //   return this.http.delete<number>(this.apiUrl + "/delete", productId, httpOptions);
  // }

  ngOnInit(): void {
    this.http.get<any>(this.apiUrl + "/all")
      .subscribe(data => {
        this.products = data;
      });
  }
}