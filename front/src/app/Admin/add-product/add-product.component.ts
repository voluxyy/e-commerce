import { NgFor } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule, NgFor],
})
export class AddProductComponent {
  categories: any;
  categoryApiUrl: string;
  productApiUrl: string;

  gameForm = new FormGroup({
    name: new FormControl(''),
    price: new FormControl(''),
    quantity: new FormControl(''), 
    categoryId: new FormControl(''),
    // imageFile: new FormControl(''),
  });

  constructor(private http: HttpClient) {
    this.productApiUrl = 'http://localhost:5016/api/Product';
    this.categoryApiUrl = 'http://localhost:5016/api/Category';
  }

  onSubmit() {
    console.log(JSON.stringify(this.gameForm.value));

    
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' })};
    console.log(httpOptions)
    this.http.post<any>(this.productApiUrl, JSON.stringify(this.gameForm.value), httpOptions)
    .subscribe(data => {
      console.log(data);
    });
  }

  ngOnInit(): void {
    this.http.get<any>(this.categoryApiUrl + "/all")
      .subscribe(data => {
        this.categories = data;
      });
  }
}
