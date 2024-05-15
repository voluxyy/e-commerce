// import { NgFor } from '@angular/common';
// import { HttpClient, HttpHeaders } from '@angular/common/http';
// import { NONE_TYPE } from '@angular/compiler';
// import { Component, Input } from '@angular/core';
// import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

// @Component({
//   selector: 'app-add-product',
//   templateUrl: './add-product.component.html',
//   styleUrls: ['./add-product.component.scss'],
//   standalone: true,
//   imports: [ReactiveFormsModule, NgFor],
// })
// export class AddProductComponent {
//   categories: any;
//   categoryApiUrl: string;
//   productApiUrl: string;

//   form: FormGroup<{
//     Name: FormControl<string | null>;
//     Price: FormControl<number | null>;
//     Quantity: FormControl<number | null>; 
//     CategoryId: FormControl<number | null>;
//     ImageFile: FormControl;
//   }>;

//   constructor(private formBuilder: FormBuilder, private http: HttpClient) {
//     this.productApiUrl = 'http://localhost:5016/api/Product';
//     this.categoryApiUrl = 'http://localhost:5016/api/Category';

//     this.form = this.formBuilder.group({
//       Name: new FormControl<string | null>(null),
//       Price: new FormControl<number | null>(null),
//       Quantity: new FormControl<number | null>(null),
//       CategoryId: new FormControl<number | null>(null),
//       ImageFile: new FormControl<File | null>(null),
//     });
//   }

//   onFileSelected(event: Event) {
//     const fileInput = event.target as HTMLInputElement;
//     if (fileInput.files && fileInput.files.length > 0) {
//       const file = fileInput.files[0];
//       this.form.patchValue({ ImageFile: file });
//     }
//   }

//   onSubmit() {
//     const formData = new FormData();
//     formData.append('dto', JSON.stringify(this.form.value));

//     const imageFile = (this.form.get('ImageFile')?.value as File);

//     if (imageFile instanceof File) {
//       formData.append('ImageFile', imageFile, imageFile.name);
//     } else {
//       console.error("Format non valide");
//       return;
//     }

//     formData.append('image', imageFile, imageFile.name);

//     const headers = new HttpHeaders();

//     this.http.post<any>(this.productApiUrl, formData, { headers })
//         .subscribe(data => {
//             console.log(data);
//         }, error => {
//             console.log(error);
//         });
// }

//   ngOnInit(): void {
//     this.http.get<any>(this.categoryApiUrl + "/all")
//       .subscribe(data => {
//         this.categories = data;
//       });
//   }
// }

import { NgFor } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NONE_TYPE } from '@angular/compiler';
import { Component, Input } from '@angular/core';
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

  constructor(private fb: FormBuilder, private http: HttpClient) {
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

    console.log(formData);

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
