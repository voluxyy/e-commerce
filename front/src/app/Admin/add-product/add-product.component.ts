import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

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
  
  form: FormGroup;

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
    this.productApiUrl = 'http://localhost:5016/api/Product';
    this.categoryApiUrl = 'http://localhost:5016/api/Category';
    this.form = this.fb.group({
      name: new FormControl<string | null>(null),
      price: new FormControl<number | null>(null),
      quantity: new FormControl<number | null>(null),
      categoryId: new FormControl<number | null>(null),
      imageFile: new FormControl<File | null>(null),
    });
  }

  onFileSelected(event: Event) {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      const file = fileInput.files[0];
      this.form.get('imageFile')?.setValue(file);
    } else {
      console.error("No file selected.");
    }
  }

  onSubmit() {
    const formData = new FormData();
    const dto = {
      name: this.form.value.name,
      price: this.form.value.price,
      quantity: this.form.value.quantity,
      categoryId: this.form.value.categoryId,
    };

    formData.append('dto', JSON.stringify(dto));

    const imageFile = this.form.value.imageFile;

    if (imageFile instanceof File) {
      formData.append('image', imageFile, imageFile.name);
    } else {
      console.error("Format invalide");
      return;
    }

    this.http.post<any>(this.productApiUrl, formData)
      .subscribe(data => {
        window.location.href = "gamesAdmin";
      }, error => {
        console.log(error);
      });
  }

  ngOnInit(): void {
    this.http.get<any>(this.categoryApiUrl + "/all")
      .subscribe(data => {
        this.categories = data;
      });
  }
}
