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
  reviewsUrl: string;
  userUrl: string;

  reviews: any[] = [];
  product: any;

  id: number | undefined;

  reviewsFormatted: { user: any, review: any }[] = [];

  private routeSub: Subscription = new Subscription;

  constructor(private http: HttpClient, private route: ActivatedRoute) {
    this.productUrl = 'http://localhost:5016/api/Product';
    this.reviewsUrl = 'http://localhost:5016/api/Review';
    this.userUrl = 'http://localhost:5016/api/User';
  }

  ngOnInit(): void {
    // Get the id in the URL
    this.routeSub = this.route.params.subscribe(params => {
      this.id = +params['id'];
      this.loadProductData(this.id);
    });
  }

  loadProductData(productId: number): void {
    // Get the product
    this.http.get<any>(`${this.productUrl}/get/${productId}`)
      .subscribe(data => {
        this.product = data;
      });

    // Get the reviews of the product and link them with users
    this.http.get<any[]>(`${this.reviewsUrl}/get-from-product/${productId}`)
      .subscribe(reviews => {
        this.reviews = reviews;
        this.linkReviewsWithUsers();
      });
  }

  linkReviewsWithUsers(): void {
    const requests = this.reviews.map(review => {
      return this.http.get<any>(`${this.userUrl}/get/${review.userId}`).toPromise()
        .then(user => {
          this.reviewsFormatted.push({ user, review });
        });
    });

    Promise.all(requests);
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }
}