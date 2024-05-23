import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-edit-password',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './edit-password.component.html',
  styleUrl: './edit-password.component.scss'
})
export class EditPasswordComponent {
  userUrl: string;
  form: FormGroup;
  user: any;

  private routeSub: Subscription = new Subscription;
  private id: number | undefined;

  constructor(private http: HttpClient, private fb: FormBuilder, private cookie: CookieService) {
    this.userUrl = 'http://localhost:5016/api/User';
    this.form = this.fb.group({
      id: new FormControl<number | null>(null),
      pseudo: new FormControl<string | null>(null),
      email: new FormControl<string | null>(null),
      password: new FormControl<string | null>(null),
    });
  }

  ngOnInit() {
    this.id = Number(this.cookie.get("UserId"));
    if (!this.id) {
      window.location.href = "";
      return;
    }

    this.loadProductData(this.id);
  }

  async loadProductData(id: number): Promise<void> {
    try {
      // Init the requests
      const userRequest = this.http.get<any>(`${this.userUrl}/get/${id}`).toPromise();

      // Wait the end of the requests
      const [user] = await Promise.all([userRequest]);

      this.user = user;

      // Update the form values with the retrieved product data
      this.form.patchValue({
        id: user.id,
        pseudo: user.pseudo,
        email: user.email,
        password: null,
      });
    } catch (error) {
      console.error('An error occurred:', error);
    }
  }

  onSubmit() {
    const dto = {
      id: this.user.id,
      pseudo: this.user.pseudo,
      email: this.user.email,
      password: this.form.value.password
    };

    const passwordConfEl = document.getElementById("password-confirmation") as HTMLInputElement;
    if (dto.password != passwordConfEl.value) {
      const el = document.createElement("p");
      el.innerText = "The passwords are not the same!";

      document.getElementById("errors")?.appendChild(el);
      return;
    }

    const formData = JSON.stringify(dto);
    const headers = new HttpHeaders().set("Content-Type", "application/json");

    this.http.put<any>(`${this.userUrl}/update-password/${this.id}`, formData, { headers })
      .subscribe(data => {
        window.location.href = `my-profile`;
      }, error => {
        console.log(error);
      });
  }
}
