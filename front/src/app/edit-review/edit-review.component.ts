import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-edit-review',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './edit-review.component.html',
  styleUrl: './edit-review.component.scss'
})
export class EditReviewComponent {
  reviewUrl : string;
  form: FormGroup;
  review: any;
  
  private routeSub: Subscription = new Subscription;
  private reviewId: number | undefined;

  private userId: number | undefined;

  constructor(private http: HttpClient, private fb: FormBuilder, private route: ActivatedRoute, private cookie: CookieService) {
    this.reviewUrl = "http://localhost:5016/api/Review";
    this.form = this.fb.group({
      title: new FormControl<number | null>(null),
      description: new FormControl<number | null>(null),
      rate: new FormControl<number | null>(null),
      productId: new FormControl<number | null>(null),
    });
  }

  ngOnInit() {
    this.userId = Number(this.cookie.get("UserId"));
    if (!this.userId) {
      window.location.href = "";
      return;
    }
    
    this.routeSub = this.route.params.subscribe(async params => {
      this.reviewId = +params['id'];
      await this.loadProductData(this.reviewId);
    });
  }

  async loadProductData(id: number): Promise<void> {
    try {
      // Init the requests
      const reviewRequest = this.http.get<any>(`${this.reviewUrl}/get/${id}`).toPromise();

      // Wait the end of the requests
      const [review] = await Promise.all([reviewRequest]);

      this.review = review;

      // Update the form values with the retrieved product data
      this.form.patchValue({
        id: review.id,
        title: review.title,
        description: review.description,
        rate: review.rate,
        productId: review.productId,
        userId: review.userId,
      });
    } catch (error) {
      console.error('An error occurred:', error);
    }
  }

  onSubmit() {
    const dto = {
      id: this.reviewId,
      Title: this.form.value.title,
      Description: this.form.value.description,
      Rate: this.form.value.rate,
      ProductId: this.form.value.productId,
      UserId: this.userId,
    }

    if (dto.Rate > 5) {
      
    }

    const formData = JSON.stringify(dto);
    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    this.http.put<any>(`${this.reviewUrl}/update/${this.reviewId}`, formData, { headers })
      .subscribe(data => {
        window.location.href = `product/${this.form.value.productId}`;
      }, error => {
        console.log(error);
      });
  }
}