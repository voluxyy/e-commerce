import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { last, lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-members-admin',
  standalone: true,
  imports: [NgFor, RouterLink],
  templateUrl: './members-admin.component.html',
  styleUrl: './members-admin.component.scss'
})
export class MembersAdminComponent {
  userUrl: string;
  saleUrl: string;
  productUrl: string;

  usersFormatted: any = [];

  constructor(private http: HttpClient) {
    this.userUrl = 'http://localhost:5016/api/User';
    this.saleUrl = 'http://localhost:5016/api/Sale';
    this.productUrl = 'http://localhost:5016/api/Product';
  }


  async ngOnInit(): Promise<void> {
    const usersRequest = this.http.get<any>(`${this.userUrl}/all`);
    let users: any[] = await lastValueFrom(usersRequest);
    
    users.forEach((u: any) => {
      this.http.get<any>(`${this.saleUrl}/get-from-user/${u.id}`)
        .subscribe(data => {
          let sales: any[] = [];
          data.forEach((s: any) => {
            this.http.get<any>(`${this.productUrl}/get/${s.productId}`)
              .subscribe(product => {
                sales.push({
                  value: s,
                  product: product,
                });
              });
          });

          this.usersFormatted.push({
            user: u,
            sales: sales,
          });
        });
    });
  }
}
