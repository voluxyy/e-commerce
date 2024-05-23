import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

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
  categorie: any;
  
  private routeSub: Subscription = new Subscription;
  private catId: number | undefined;

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) {
    this.url = "http://localhost:5016/api/Category";
    this.form = this.fb.group({
      id: new FormControl<number | null>(null),
      categoryName: new FormControl<string | null>(null),
    });
  }

  ngOnInit() {
    this.routeSub = this.route.params.subscribe(async params => {
      this.catId = +params['id'];
      await this.loadProductData(this.catId);
    });
  }

  async loadProductData(id: number): Promise<void> {
    try {
      // Init the requests
      const categorieRequest = this.http.get<any>(`${this.url}/get/${id}`).toPromise();

      // Wait the end of the requests
      const [categorie] = await Promise.all([categorieRequest]);

      this.categorie = categorie;

      // Update the form values with the retrieved product data
      this.form.patchValue({
        id: categorie.id,
        categoryName: categorie.categoryName,
      });
    } catch (error) {
      console.error('An error occurred:', error);
    }
  }

  onSubmit() {
    const dto = {
      id: this.form.value.id,
      categoryName: this.form.value.categoryName,
    }

    const formData = JSON.stringify(dto);
    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    this.http.put<any>(`${this.url}/update/${this.catId}`, formData, { headers })
      .subscribe(data => {
        window.location.href = "gamesAdmin";
      }, error => {
        console.log(error);
      });
  }
}