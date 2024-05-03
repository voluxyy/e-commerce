import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RouterOutlet } from '@angular/router';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-card-bento',
  standalone: true,
  imports: [RouterOutlet, NgFor],
  templateUrl: './card-bento.component.html',
  styleUrl: './card-bento.component.scss'
})
export class CardBentoComponent {
  products: any;
  target: string;

  constructor(private http: HttpClient) {
    this.target = 'http://localhost:5016';
  }

  ngOnInit(): void {
    this.http.get<any>(this.target + "/api/Product/all")
      .subscribe(data => {
        this.products = data;
      });
  }
}