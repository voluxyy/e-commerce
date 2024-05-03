import { CommonModule, NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-card',
  standalone: true,
  imports: [CommonModule, NgFor],
  templateUrl: './card.component.html',
  styleUrl: './card.component.scss'
})
export class CardComponent implements OnInit {
  products: any;
  name: any;
  imagePath: any;
  price: any;
  quantity: any;
  category: any;
  target: string;

  constructor(private http: HttpClient) {
    this.target = 'http://localhost:5016';
  }

  ngOnInit(): void {
    this.http.get<any>(this.target + "/api/Product/all")
      .subscribe(resp => {
        this.products = resp;
        this.imagePath = resp;
        this.name = resp;
        this.price = resp;
        this.quantity = resp;
        this.category = resp;
      });
  }
}
