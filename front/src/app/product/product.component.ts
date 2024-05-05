import { NgFor, NgIf } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-games-admin',
  standalone: true,
  imports: [RouterOutlet, NgFor, NgIf],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss'
})
export class ProductComponent {
  products: any;
  prod: any;
  apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = 'http://localhost:5016/Product/:id';
  }

  addGame(prod: any): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<any>(this.apiUrl + "/api/Product/:id", prod, httpOptions);
  }

  ngOnInit(): void {
    this.http.get<any>(this.apiUrl + "/api/Product/all")
      .subscribe(data => {
        this.products = data;
      });
  }
}