import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-category',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './edit-category.component.html',
  styleUrl: './edit-category.component.scss'
})
export class EditCategoryComponent {
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

    this.http.put<any>(this.url, formData, { headers })
      .subscribe(data => {
        this.router.navigate(['gamesAdmin']);
      }, error => {
        console.log(error);
      });
  }
}