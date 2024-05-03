import { HttpClient } from '@angular/common/http';
import { Component, NgModule, OnInit, viewChild } from '@angular/core';
import { ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})

export class HomeComponent implements OnInit {
  productTitle: any;
  productDescription: any;
  productPrice: any;
  productRate: any;
  productImage: any;

  target: string;

  constructor(private http: HttpClient) {
    this.target = 'https://127.0.0.1:7094';
    
  }

  ngOnInit(): void {
    this.http.get<any>(this.target+"/api/Product/all")
    .subscribe(data => {
      this.productTitle = data.title;
      this.productDescription = data.description;
      this.productPrice = data.price;
      this.productRate = data.rate;
      this.productImage = data.image;
    });

  }
}
