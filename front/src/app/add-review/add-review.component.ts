import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-review',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './add-review.component.html',
  styleUrl: './add-review.component.scss'
})
export class AddReviewComponent {
    reviewUrl : string;
    form: FormGroup;
    categorie: any;
    
    private routeSub: Subscription = new Subscription;
    private productId: number | undefined;

    private userId: number | undefined;
  
    constructor(private http: HttpClient, private fb: FormBuilder, private route: ActivatedRoute, private cookie: CookieService) {
      this.reviewUrl = "http://localhost:5016/api/Review";
      this.form = this.fb.group({
        title: new FormControl<number | null>(null),
        description: new FormControl<number | null>(null),
        rate: new FormControl<number | null>(null),
      });
    }
  
    ngOnInit() {
      this.userId = Number(this.cookie.get("UserId"));
      if (!this.userId) {
        window.location.href = "";
        return;
      }
      
      this.routeSub = this.route.params.subscribe(async params => {
        this.productId = +params['id'];
      });
    }
  
    onSubmit() {
      const dto = {
        Title: this.form.value.title,
        Description: this.form.value.description,
        Rate: this.form.value.rate,
        ProductId: this.productId,
        UserId: this.userId,
      }
  
      const formData = JSON.stringify(dto);
      const headers = new HttpHeaders().set('Content-Type', 'application/json');
  
      this.http.post<any>(`${this.reviewUrl}`, formData, { headers })
        .subscribe(data => {
          window.location.href = `product/${this.productId}`;
        }, error => {
          console.log(error);
        });
    }
  }