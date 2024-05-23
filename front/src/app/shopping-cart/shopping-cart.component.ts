import { NgFor, NgIf } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-shopping-cart',
  standalone: true,
  imports: [NgFor, NgIf],
  templateUrl: './shopping-cart.component.html',
  styleUrl: './shopping-cart.component.scss'
})
export class ShoppingCartComponent {
  productListUrl: string;
  shoppingCartUrl: string;
  productUrl: string;
  userUrl: string;
  saleUrl: string;

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
    this.saleUrl = 'http://localhost:5016/api/Sale'
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

  async purchase() {
    if (this.user.money >= this.totalPrice) {
      const headers = new HttpHeaders().set("Content-Type", "application/json");

      this.products.forEach(async (product :any) => {
        if (this.user.money >= product.price) {
          if (product.quantity > 0) {
            const saleDto = {
              UserId: this.userId,
              ProductId: product.id
            };

            const formSaleData = JSON.stringify(saleDto);
            const saleRequest = this.http.post<any>(this.saleUrl, formSaleData, { headers });

            const deleteProductListRequest = this.http.delete(`${this.productListUrl}/delete-from-product/${product.id}`);

            this.user.money -= product.price;
            const moneyDto = {
              UserId: this.userId,
              Money: this.user.money,
            }

            const formMoneyData = JSON.stringify(moneyDto);
            const moneyRequest = this.http.put<any>(`${this.userUrl}/update-money/${this.userId}`, formMoneyData, { headers });

            try {
              await lastValueFrom(saleRequest);
              await lastValueFrom(moneyRequest);
              await lastValueFrom(deleteProductListRequest);
            } catch (error) {
                console.error(`Error purchasing product: ${product.name}`, error);
            }
            
            this.products = this.products.filter((p: { id: number; }) => p.id !== product.id);
          } else {
            console.log("Not enough quantitor for: " + product.name);
          }
        }
      });
    } else {
      console.log("Error not enough money!")
    }
  }
}