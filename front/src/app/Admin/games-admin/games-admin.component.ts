import { NgFor, NgIf } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-games-admin',
  standalone: true,
  imports: [RouterOutlet, NgFor, NgIf, RouterLink],
  templateUrl: './games-admin.component.html',
  styleUrl: './games-admin.component.scss'
})
export class GamesAdminComponent {
  products: any;
  apiUrl: string;

<<<<<<< Updated upstream
  constructor(private http: HttpClient) {
    this.apiUrl = 'http://localhost:5016/api/Product';
  }

  deleteGame(productId: number) {
    this.http.delete(this.apiUrl + '/delete/' + productId).subscribe(() => {
      this.products = this.products.filter((product: { id: number; }) => product.id !== productId);
    }, error => {
      console.error('Error deleting product:', error);
    });
  }
=======
  // id: number;

  constructor(private http: HttpClient) {
    this.apiUrl = 'http://localhost:5016/api/Product';
    // this.id = this.products.id;
  }

  // deleteGame() {
  //   return this.http.delete<number>(this.apiUrl + "/delete/" + this.id);
  // }
>>>>>>> Stashed changes


  ngOnInit(): void {
    this.http.get<any>(this.apiUrl + "/all")
      .subscribe(data => {
        this.products = data;
      });
<<<<<<< Updated upstream
=======
    // this.http.delete<number>(this.apiUrl + "/delete/" + this.id)
    //   .subscribe(data => {
    //     this.products = data;
    //   });
>>>>>>> Stashed changes
  }
}