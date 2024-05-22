import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-shopping-cart',
  standalone: true,
  imports: [NgFor],
  templateUrl: './shopping-cart.component.html',
  styleUrl: './shopping-cart.component.scss'
})
export class ShoppingCartComponent {
  productListUrl: string;
  shoppingCartUrl: string;
  productUrl: string;

  shoppingCartId: any;
  userId: number;

  products: any;

  constructor(private http: HttpClient, private cookieService: CookieService) {
    this.productUrl = 'http://localhost:5016/api/Product';
    this.productListUrl = 'http://localhost:5016/api/ProductList';
    this.shoppingCartUrl = 'http://localhost:5016/api/ShoppingCart';
    this.userId = Number(cookieService.get('UserId'))
    this.products = [];
  }

  async ngOnInit(): Promise<void> {
    try {
      const shoppingCartRequest = this.http.get<any>(`${this.shoppingCartUrl}/get-from-user/${this.userId}`);
      const shoppingCart = await lastValueFrom(shoppingCartRequest);
      this.shoppingCartId = shoppingCart.id;

      const productListRequest = this.http.get<any>(`${this.productListUrl}/get-from-shopping-cart/${this.shoppingCartId}`);
      const productLists = await lastValueFrom(productListRequest);

      for (let productList of productLists) {
        const productRequest = this.http.get<any>(`${this.productUrl}/get/${productList.productId}`);
        this.products.push(await lastValueFrom(productRequest));
      }
    } catch (error) {
      console.error('An error occurred:', error);
    }
  }

  deleteProduct(id: number) {
    this.http.delete<any>(`${this.productListUrl}/delete-from-product/${id}`)
      .subscribe(() => {
        this.products = this.products.filter((product: { id: number; }) => product.id !== id);
      }, error => {
        console.error(error);
      });
  }
}