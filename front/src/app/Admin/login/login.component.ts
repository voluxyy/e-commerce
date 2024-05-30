import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { delay } from 'rxjs';

@Component({
  selector: 'app-admin-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class AdminLoginComponent {
  url: string;
  form: FormGroup;
  errorMessage: string | null = null;

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router, private cookieService: CookieService) {
    this.url = "http://localhost:5016/api/Admin";
    this.form = this.fb.group({
      email: new FormControl<string | null>(null),
      password: new FormControl<string | null>(null),
    });
  }

  onSubmit() {
    const dto = {
      email: this.form.value.email,
      password: this.form.value.password,
    };

    const formData = JSON.stringify(dto);
    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    this.http.post<any>(this.url + '/check-connection', formData, { headers })
      .subscribe({
        next: (data) => {
          this.cookieService.set('Type', 'Admin');
          this.cookieService.set('UserId', data.id);
          this.router.navigate(['/gamesAdmin']);
        },
        error: (error) => {
          console.error('Login failed', error);
          this.errorMessage = 'Invalid email or password. Please try again.';
        }
      });
  }
}
