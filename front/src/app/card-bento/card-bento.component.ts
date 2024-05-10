import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RouterOutlet } from '@angular/router';
import { NgFor } from '@angular/common';
import {RouterModule} from '@angular/router';

@Component({
  selector: 'app-card-bento',
  standalone: true,
  imports: [RouterOutlet, NgFor],
  templateUrl: './card-bento.component.html',
  styleUrl: './card-bento.component.scss'
})
export class CardBentoComponent {
  products: any;
  apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = 'http://localhost:5016/api/Product';
  }

  // getProductsbyId(id: number) {
  //   this.http.get<any>(this.apiUrl + '/' + id)
  //     .subscribe(
  //       data => {
  //         this.products = data;
  //       },
  //     );
  // }

  ngOnInit(): void {
    this.http.get<any>(this.apiUrl + '/all')
      .subscribe(data => {
        this.products = data;
      });
  }
}