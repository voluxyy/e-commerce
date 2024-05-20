import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-shopping-cart',
  standalone: true,
  imports: [NgFor],
  templateUrl: './shopping-cart.component.html',
  styleUrl: './shopping-cart.component.scss'
})
export class ShoppingCartComponent {
  productList: any;
  productListUrl: string;

  shoppingCart: any;
  shoppingCartUrl: string;

  constructor(private http: HttpClient) {
    this.productListUrl = 'http://localhost:5016/api/ProductList';
    this.shoppingCartUrl = 'http://localhost:5016/api/ShoppingCart';
  }


  ngOnInit(): void {
    this.http.get<any>(this.productListUrl + "/all")
      .subscribe(data => {
        this.productList = data;
      });
    this.http.get<any>(this.shoppingCartUrl + "/all")
      .subscribe(data => {
        this.shoppingCart = data;
      });
  }
}