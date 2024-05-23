import { NgFor } from '@angular/common';
import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';

@Component({
  selector: 'app-edit-product',
  standalone: true,
  imports: [ReactiveFormsModule, NgFor],
  templateUrl: './edit-product.component.html',
  styleUrl: './edit-product.component.scss'
})
export class EditProductComponent {
  categories: any;
  categoryApiUrl: string;
  productApiUrl: string;

  private routeSub: Subscription = new Subscription;
  private id: number | undefined;
  product: any;

  form: FormGroup;

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) {
    this.productApiUrl = 'http://localhost:5016/api/Product';
    this.categoryApiUrl = 'http://localhost:5016/api/Category';
    this.form = this.fb.group({
      id: new FormControl<number | null>(null),
      name: new FormControl<string | null>(null),
      price: new FormControl<number | null>(null),
      quantity: new FormControl<number | null>(null),
      categoryId: new FormControl<number | null>(null),
      imageFile: new FormControl<File | null>(null),
    });
  }

  async ngOnInit(): Promise<void> {
    // Get the id in the URL
    this.routeSub = this.route.params.subscribe(async params => {
      this.id = +params['id'];
      await this.loadProductData(this.id);
    });
  }

  async loadProductData(productId: number): Promise<void> {
    try {
      // Init the requests
      const categoryRequest = this.http.get<any>(this.categoryApiUrl + "/all").toPromise();
      const productRequest = this.http.get<any>(`${this.productApiUrl}/get/${productId}`).toPromise();

      // Wait the end of the requests
      const [categories, product] = await Promise.all([categoryRequest, productRequest]);

      this.categories = categories;
      this.product = product;

      // Update the form values with the retrieved product data
      this.form.patchValue({
        id: product.id,
        name: product.name,
        price: product.price,
        quantity: product.quantity,
        categoryId: product.categoryId,
      });

      // Show the current image
      document.getElementById("current-image")?.setAttribute("src", "../assets/" + product.imagePath);
    } catch (error) {
      console.error('An error occurred:', error);
    }
  }

  onFileSelected(event: Event) {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      const file = fileInput.files[0];
      this.form.get('imageFile')?.setValue(file);

      document.getElementById("current-image")?.setAttribute("src", "../assets/" + file);
    } else {
      console.error("No file selected.");
    }
  }

  onSubmit() {
    const formData = new FormData();
    const dto = {
      id: this.form.value.id,
      name: this.form.value.name,
      price: this.form.value.price,
      quantity: this.form.value.quantity,
      categoryId: this.form.value.categoryId,
    };

    formData.append('dto', JSON.stringify(dto));

    const imageFile = this.form.value.imageFile;

    if (imageFile instanceof File) {
      formData.append('image', imageFile, imageFile.name);
    } else if (imageFile != null || imageFile != undefined) {
      console.error("Format invalide");
      return;
    }

    this.http.put<any>(this.productApiUrl + "/update/" + dto.id, formData)
      .subscribe(data => {
        window.location.href = "gamesAdmin";
      }, error => {
        console.log(error);
      });
  }
}