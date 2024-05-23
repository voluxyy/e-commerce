import { CommonModule, NgFor, NgIf } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ArrayType } from '@angular/compiler';
import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { last, lastValueFrom, Observable } from 'rxjs';

@Component({
  selector: 'app-games-admin',
  standalone: true,
  imports: [RouterOutlet, NgFor, NgIf, RouterLink],
  templateUrl: './games-admin.component.html',
  styleUrl: './games-admin.component.scss'
})
export class GamesAdminComponent {
  categories: any;
  productUrl: string;
  reviewUrl: string;
  categoryUrl: string;

  values: any = [];

  constructor(private http: HttpClient) {
    this.productUrl = 'http://localhost:5016/api/Product';
    this.reviewUrl = 'http://localhost:5016/api/Review';
    this.categoryUrl = 'http://localhost:5016/api/Category';
  }

  deleteGame(productId: number) {
    this.http.delete(this.productUrl + '/delete/' + productId).subscribe(() => {
      this.values = this.values.filter((value: { product: any; }) => value.product.id !== productId);
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


  async ngOnInit(): Promise<void> {
    const productsRequest = this.http.get<any>(`${this.productUrl}/all`);
    let products = await lastValueFrom(productsRequest);

    for (let product of products) {
      const reviewRequest = this.http.get<any>(`${this.reviewUrl}/get-average-rate/${product.id}`);
      let rate = await lastValueFrom(reviewRequest);
      this.values.push(
        {
          product: product,
          rate: rate,
        }
      )
    }

    const categoryRequest = this.http.get<any>(`${this.categoryUrl}/all`);
    this.categories = await lastValueFrom(categoryRequest);
  }
}