import { NgFor, NgIf } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, RouterOutlet } from '@angular/router';
import { Subscription } from 'rxjs';


@Component({
  selector: 'product',
  standalone: true,
  imports: [RouterOutlet, NgFor, NgIf],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss'
})
export class ProductComponent {
  productUrl: string;
  commentsUrl: string;
  rateUrl: string;
  userUrl: string;

  comments: any;
  rates: any;
  product: any;

  id: number | undefined;

  commentsFormatted: [
    {
      user: object,
      comment: object,
      rate: object,
    }
  ] | null;


  private routeSub: Subscription = new Subscription;

  constructor(private http: HttpClient, private route: ActivatedRoute) {
    this.productUrl = 'http://localhost:5016/api/Product';
    this.commentsUrl = 'http://localhost:5016/api/Comment';
    this.rateUrl = 'http://localhost:5016/api/Rate';
    this.userUrl = 'http://localhost:5016/api/User';
    this.commentsFormatted = null;
  }

  ngOnInit(): void {
    // Get the id in the URL
    this.routeSub = this.route.params.subscribe(params => {
      this.id = params['id'];
    });

    // Get the product
    this.http.get<any>(this.productUrl + '/get/' + this.id)
      .subscribe(data => {
        this.product = data;
      });

    // TODO: create backend routes to get from product comments and rates

    // Get the comments of the product
    this.http.get<any>(this.commentsUrl + '/get-from-product/' + this.id)
      .subscribe(data => {
        this.comments = data;
      });

    // Get the rates of the product
    this.http.get<any>(this.rateUrl + '/get-from-product/' + this.id)
      .subscribe(data => {
        this.rates = data;
      });

    // Link rate and comment
    for (let comment of this.comments) {
      let currentRate: any;
      let currentUser: any;
      for (let rate of this.rates) {
        if (rate.UserId == comment.UserId) {
          currentRate = rate;
        }

        this.http.get<any>(this.userUrl + '/get/' + comment.UserId)
          .subscribe(data => {
            currentUser = data;
          });
      }
      this.commentsFormatted?.push({
        user: currentUser,
        comment: comment,
        rate: currentRate
      });
    }
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }
}