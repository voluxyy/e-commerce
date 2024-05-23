import { CommonModule, NgFor, NgIf } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-games-admin',
  standalone: true,
  imports: [RouterOutlet, NgFor, NgIf, RouterLink],
  templateUrl: './games-admin.component.html',
  styleUrl: './games-admin.component.scss'
})
export class GamesAdminComponent {
  products: any;
  categories: any;
  apiUrl: string;
  categoryUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = 'http://localhost:5016/api/Product';
    this.categoryUrl = 'http://localhost:5016/api/Category';
  }

  deleteGame(productId: number) {
    this.http.delete(this.apiUrl + '/delete/' + productId).subscribe(() => {
      this.products = this.products.filter((product: { id: number; }) => product.id !== productId);
    }, error => {
      console.error('Error deleting product:', error);
    });
  }

  deleteCategory(categoryId: number) {
    this.http.delete(this.categoryUrl + '/delete/' + categoryId).subscribe(() => {
      this.categories = this.categories.filter((category: { id: number; }) => category.id !== categoryId);
    }, error => {
      console.error('Error deleting product:', error);
    });
  }


  ngOnInit(): void {
    this.http.get<any>(this.apiUrl + "/all")
      .subscribe(data => {
        this.products = data;
      });
    this.http.get<any>(this.categoryUrl + "/all")
      .subscribe(data => {
        this.categories = data;
      });
  }
}