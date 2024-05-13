import { NgFor } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NONE_TYPE } from '@angular/compiler';
import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

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

  form: FormGroup<{
    Name: FormControl<string | null>;
    Price: FormControl<number | null>;
    Quantity: FormControl<number | null>; 
    CategoryId: FormControl<number | null>;
    ImageFile: FormControl;
  }> = new FormGroup({
    Name: new FormControl<string | null>(null),
    Price: new FormControl<number | null>(null),
    Quantity: new FormControl<number | null>(null), 
    CategoryId: new FormControl<number | null>(null),
    ImageFile: new FormControl(null),
  });

  constructor(private http: HttpClient) {
    this.productApiUrl = 'http://localhost:5016/api/Product';
    this.categoryApiUrl = 'http://localhost:5016/api/Category';
  }

  onSubmit() {
    const formData = JSON.stringify(this.form.value);
    const headers = new HttpHeaders().set('Content-Type', 'multipart/form-data');

    // J'ai réussi à créer un produit en mettant Content-type: application/json
    // Et côté backend en faisant en sorte de ne pas prendre en compte l'image.
    // Cet à dire que j'ai commenté toutes les lignes faisant référence à l'image
    // et j'ai ajouté une variable temporaire comme ceci : //byte[] imageData = new byte[2];
    // Pour que cette ligne : await this.service.Add(dto, imageData); n'est pas d'erreur.

    this.http.post<any>(this.productApiUrl, formData, { headers })
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
