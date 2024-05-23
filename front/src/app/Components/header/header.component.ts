import {  HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  url: string;
  result: any;

  id: number | undefined;

  constructor(private http: HttpClient, private cookie: CookieService) {
    this.url = 'http://localhost:5016/api/Product';
  }

  ngOnInit() {
    this.id = Number(this.cookie.get("UserId"));
  }

  async searchbar() {
    const input = document.getElementById("searchbar") as HTMLInputElement;
    let value = input?.value;

    if (value == null || value.trim() === "")
      return;

    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    const request = this.http.get<any>(this.url + '/searchbar/' + value, { headers });

    this.result = await lastValueFrom(request);
    this.showResult();
  }

  showResult() {
    const container = document.getElementById("searchbar-result-container") as HTMLElement;

    container.innerHTML = ''; // Clear previous results

    this.result.forEach((product: { name: any; price: any; }) => {
      const productElement = document.createElement('div');
      productElement.className = 'search-result-item';
      productElement.innerHTML = `
        <h3>${product.name}</h3>
        <p>${product.price} €</p>
      `;
      container.appendChild(productElement);
    });
  }
}
