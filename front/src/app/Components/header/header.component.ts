import { NgIf } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterModule, NgIf],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  url: string;
  result: any;

  id: string | undefined;
  connected: boolean = false;
  type: string = "User";

  constructor(private http: HttpClient, private cookie: CookieService) {
    this.url = 'http://localhost:5016/api/Product';
  }

  ngOnInit() {
    this.id = this.cookie.get("UserId");
    this.type = this.cookie.get("Type");
    if (this.id)
      this.connected = true;
  }

  async searchbar() {
    const input = document.getElementById("searchbar") as HTMLInputElement;
    let value = input?.value;

    if (value == null || value.trim() === "") {
      const container = document.getElementById("searchbar-result-container") as HTMLElement;
      container.innerHTML = '';
      return;
    }

    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    const request = this.http.get<any>(this.url + '/searchbar/' + value, { headers });

    this.result = await lastValueFrom(request);
    this.showResult();
  }

  showResult() {
    const container = document.getElementById("searchbar-result-container") as HTMLElement;

    container.innerHTML = '';

    this.result.forEach((product: { name: any; price: any; imagePath: any; id: any }) => {
      const productElement = document.createElement('a');
      productElement.href = '/product/' + product.id;
      productElement.className = 'search-result-item';

      productElement.style.width = '100%';
      productElement.style.display = 'flex';
      productElement.style.flexDirection = 'column';
      productElement.style.padding = '1% 4%';
      productElement.style.textDecoration = 'none';
      productElement.style.color = 'var(--var-grey)';
      productElement.style.padding = '20% 4%';

      const imageContainer = document.createElement('div');
      imageContainer.style.width = '210px';
      imageContainer.style.height = '180px';
      
      const imageElement = document.createElement('img');
      imageElement.src = '../assets/' + product.imagePath;
      imageElement.style.width = '100%';
      imageElement.style.height = '100%';
      imageElement.style.borderRadius = '20px';
      imageElement.style.objectFit = 'cover';

      imageContainer.appendChild(imageElement);
      productElement.appendChild(imageContainer);
      productElement.innerHTML += `
        <h3 style="margin:0;padding:0;word-break: break-all; font-size:1rem;">${product.name}</h3>
        <p style="margin:0;padding:0;">${product.price} €</p>
      `;
      container.appendChild(productElement);
    });
  }

  deconnection() {
    this.cookie.deleteAll();
    window.location.href = "";
  }
}
