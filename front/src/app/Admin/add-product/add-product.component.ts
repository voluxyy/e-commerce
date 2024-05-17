import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

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

  constructor(private http: HttpClient, private fb: FormBuilder) {
    this.productApiUrl = 'http://localhost:5016/api/Product';
    this.categoryApiUrl = 'http://localhost:5016/api/Category';
    this.form = this.fb.group({
      Name: new FormControl<string | null>(null),
      Price: new FormControl<number | null>(null),
      Quantity: new FormControl<number | null>(null),
      CategoryId: new FormControl<number | null>(null),
      ImageFile: new FormControl<File | null>(null),
    });
  }

  onFileSelected(event: Event) {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      const file = fileInput.files[0];
      console.log("File selected:", file);
      this.form.get('ImageFile')?.setValue(file);
    } else {
      console.error("No file selected.");
    }
  }

  onSubmit() {
    const formData = new FormData();
    const dto = {
      Name: this.form.value.Name,
      Price: this.form.value.Price,
      Quantity: this.form.value.Quantity,
      CategoryId: this.form.value.CategoryId,
    };

    formData.append('dto', JSON.stringify(dto));

    const imageFile = this.form.value.ImageFile;

    if (imageFile instanceof File) {
      formData.append('image', imageFile, imageFile.name);
    } else {
      console.error("Format invalide");
      return;
    }

    this.http.post<any>(this.productApiUrl, formData)
      .subscribe(data => {
        console.log(data);
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
