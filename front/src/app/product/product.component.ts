import { NgFor, NgIf } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterLink, RouterOutlet } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { lastValueFrom, Subscription } from 'rxjs';


@Component({
  selector: 'product',
  standalone: true,
  imports: [RouterOutlet, NgFor, NgIf, RouterLink],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss'
})
export class ProductComponent {
  productUrl: string;
  reviewUrl: string;
  userUrl: string;
  shoppingCartUrl: string;
  productListUrl: string;
  saleUrl: string;

  product: any;
  rate: any;
  reviews: any[] = [];

  canPostReview: boolean | undefined;
  userId: number | undefined;
  productId: number | undefined;

  reviewsFormatted: { user: any, review: any }[] = [];

  private routeSub: Subscription = new Subscription;

  constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute, private cookie: CookieService) {
    this.productUrl = 'http://localhost:5016/api/Product';
    this.reviewUrl = 'http://localhost:5016/api/Review';
    this.userUrl = 'http://localhost:5016/api/User';
    this.shoppingCartUrl = 'http://localhost:5016/api/ShoppingCart';
    this.productListUrl = 'http://localhost:5016/api/ProductList';
    this.saleUrl = 'http://localhost:5016/api/Sale';
    this.userId = Number(this.cookie.get("UserId"));
  }

  async ngOnInit(): Promise<void> {
    // Get the id in the URL
    this.routeSub = this.route.params.subscribe(params => {
      this.productId = +params['id'];
    });

    // If the id in url is wrong redirect to home page
    if (!this.productId || this.productId == undefined) {
      window.location.href = "";
    }

    await this.loadProductData(this.productId!);
  }

  async loadProductData(productId: number): Promise<void> {
    // Get the product
    const productRequest = this.http.get<any>(`${this.productUrl}/get/${productId}`)
    this.product = await lastValueFrom(productRequest);

    const rateRequest = this.http.get<any>(`${this.reviewUrl}/get-average-rate/${this.product.id}`);
    this.rate = await lastValueFrom(rateRequest);

    // Get the reviews of the product and link them with users
    const reviewRequest = this.http.get<any[]>(`${this.reviewUrl}/get-from-product/${productId}`);
    this.reviews = await lastValueFrom(reviewRequest);
    
    this.linkReviewsWithUsers();

    const saleRequest = this.http.get<any>(`${this.saleUrl}/has-buy/${productId}/${this.userId}`);
    this.canPostReview = await lastValueFrom(saleRequest);

    this.reviewsFormatted.forEach(reviewFormat => {
      console.log(reviewFormat);
      if (reviewFormat.review.userId == this.userId) {
        this.canPostReview = false;
      }
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

  deleteReview(id: number) {
    this.http.delete(this.reviewUrl + '/delete/' + id).subscribe(() => {
      this.reviewsFormatted = this.reviewsFormatted.filter((x: { review: any, user: any }) => x.review.id !== id);
      window.location.href = `product/${this.productId}`;
    }, error => {
      console.error('Error deleting product:', error);
    });
  }
}