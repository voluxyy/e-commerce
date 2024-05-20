import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  users: any;
  userUrl: string;

  sales: any;
  salesUrl: string;

  constructor(private http: HttpClient) { 
    this.userUrl = 'http://localhost:5016/api/User';
    this.salesUrl = 'http://localhost:5016/api/Sale';
  }

  ngOnInit(): void {
    this.http.get<any>(this.userUrl + "/all")
      .subscribe(data => {
        this.users = data;
      });
    this.http.get<any>(this.salesUrl + "/all")
      .subscribe(data => {
        this.sales = data;
      });
  }

  numberOfMembers(): number {
    return this.users.length;
  }

  numberOfSales(): number {
    return this.sales.length;
  }

  lastSevenDaysSales(): number {
    let lastSevenDaysSales = 0;
    for (let sale of this.sales) {
      let saleDate = new Date(sale.date);
      let today = new Date();
      let sevenDaysAgo = new Date(today.setDate(today.getDate() - 7));
      if (saleDate >= sevenDaysAgo) {
        lastSevenDaysSales++;
      }
    }
    return lastSevenDaysSales;
  }

  lastSevenDaysRevenues(): number {
    let lastSevenDaysRevenues = 0;
    for (let sale of this.sales) {
      let saleDate = new Date(sale.date);
      let today = new Date();
      let sevenDaysAgo = new Date(today.setDate(today.getDate() - 7));
      if (saleDate >= sevenDaysAgo) {
        lastSevenDaysRevenues += sale.total;
      }
    }
    return lastSevenDaysRevenues;
  }

  totalRevenues(): number {
    let totalRevenues = 0;
    for (let sale of this.sales) {
      totalRevenues += sale.total;
    }
    return totalRevenues;
  }

}
