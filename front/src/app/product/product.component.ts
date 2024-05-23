import { NgFor, NgIf } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, RouterOutlet } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { lastValueFrom, Subscription } from 'rxjs';


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
  shoppingCartUrl: string;
  productListUrl: string;

  product: any;
  rate: any;
  reviews: any[] = [];

  productId: number | undefined;

  reviewsFormatted: { user: any, review: any }[] = [];

  private routeSub: Subscription = new Subscription;

  constructor(private http: HttpClient, private route: ActivatedRoute, private cookie: CookieService) {
    this.productUrl = 'http://localhost:5016/api/Product';
    this.reviewsUrl = 'http://localhost:5016/api/Review';
    this.userUrl = 'http://localhost:5016/api/User';
    this.shoppingCartUrl = 'http://localhost:5016/api/ShoppingCart';
    this.productListUrl = 'http://localhost:5016/api/ProductList'
  }

  ngOnInit(): void {
    // Get the id in the URL
    this.routeSub = this.route.params.subscribe(params => {
      this.productId = +params['id'];
      this.loadProductData(this.productId);
    });
  }


  async loadProductData(productId: number): Promise<void> {
    // Get the product
    const productRequest = this.http.get<any>(`${this.productUrl}/get/${productId}`)
    this.product = await lastValueFrom(productRequest);

    const rateRequest = this.http.get<any>(`${this.reviewsUrl}/get-average-rate/${this.product.id}`);
    this.rate = await lastValueFrom(rateRequest);

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

  async addToCart() {
    let userId: number = Number(this.cookie.get("UserId"));
  
    if (isNaN(userId) || userId <= 0) {
      console.error("Invalid UserId");
      return;
    }

    const request = this.http.get<any>(`${this.shoppingCartUrl}/get-from-user/${userId}`);
    let shoppingCartId = (await lastValueFrom(request)).id;

    if (isNaN(shoppingCartId) || shoppingCartId <= 0) {
      console.error("Invalid ShoppingCartId");
      return;
    }
  
    const dto = {
      ProductId: this.productId,
      ShoppingCartId: shoppingCartId
    };
  
    const headers = new HttpHeaders().set("Content-Type", "application/json");
  
    this.http.post<any>(this.productListUrl, dto, { headers })
      .subscribe(data => {
          console.log(data);
        });
  }
}