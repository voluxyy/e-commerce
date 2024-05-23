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
  userUrl: string;

  shoppingCartId: any;
  userId: number;

  user: any;
  products: any;
  totalPrice: number;

  constructor(private http: HttpClient, private cookieService: CookieService) {
    this.productUrl = 'http://localhost:5016/api/Product';
    this.productListUrl = 'http://localhost:5016/api/ProductList';
    this.shoppingCartUrl = 'http://localhost:5016/api/ShoppingCart';
    this.userUrl = 'http://localhost:5016/api/User'
    this.userId = Number(cookieService.get('UserId'))
    this.products = [];
    this.totalPrice = 0;
  }

  async ngOnInit(): Promise<void> {
    try {
      const userRequest = this.http.get<any>(`${this.userUrl}/get/${this.userId}`);
      const user = await lastValueFrom(userRequest);
      this.user = user;

      const shoppingCartRequest = this.http.get<any>(`${this.shoppingCartUrl}/get-from-user/${this.userId}`);
      const shoppingCart = await lastValueFrom(shoppingCartRequest);
      this.shoppingCartId = shoppingCart.id;

      const productListRequest = this.http.get<any>(`${this.productListUrl}/get-from-shopping-cart/${this.shoppingCartId}`);
      const productLists = await lastValueFrom(productListRequest);

      for (let productList of productLists) {
        const productRequest = this.http.get<any>(`${this.productUrl}/get/${productList.productId}`);
        let product = await lastValueFrom(productRequest);
        this.totalPrice += product.price;
        this.products.push(product);
      }

      document.getElementById('purchase-btn')?.classList.add((this.user.money > this.totalPrice) ? "enough" : "not_enough");
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

  purchase() {
    if (this.user.money > this.totalPrice) {

    } else {
      
    }
  }
}