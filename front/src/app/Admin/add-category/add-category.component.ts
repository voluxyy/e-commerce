import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-category',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.scss'
})
export class AddCategoryComponent {
  url : string;
  form: FormGroup;

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
    this.url = "http://localhost:5016/api/Category";
    this.form = this.fb.group({
      categoryName: new FormControl<string | null>(null),
    });
  }

  onSubmit() {
    const dto = {
      categoryName: this.form.value.categoryName,
    }

    const formData = JSON.stringify(dto);
    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    console.log(formData);

    this.http.post<any>(this.url, formData, { headers })
      .subscribe(data => {
        window.location.href = "gamesAdmin";
      }, error => {
        console.log(error);
      });
  }
}
  