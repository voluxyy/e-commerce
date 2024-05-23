import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
  standalone: true,
  imports: [ReactiveFormsModule],
})

export class RegisterComponent {
  url : string;
  form: FormGroup;

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
    this.url = "http://localhost:5016/api/User";
    this.form = this.fb.group({
      lastname: new FormControl<string | null>(null),
      firstname: new FormControl<number | null>(null),
      pseudo: new FormControl<number | null>(null),
      email: new FormControl<number | null>(null),
      password: new FormControl<number | null>(null),
      birthdate: new FormControl<number | null>(null),
    });
  }

  onSubmit() {
    const formData = new FormData();
    const dto = {
      lastname: this.form.value.lastname,
      firstname: this.form.value.firstname,
      pseudo: this.form.value.pseudo,
      email: this.form.value.email,
      password: this.form.value.password,
      birthdate: this.form.value.birthdate,
    }

    formData.append('dto', JSON.stringify(dto));

    this.http.post<any>(this.url, formData)
      .subscribe(data => {
        window.location.href = "login";
      }, error => {
        console.log(error);
      });
  }
}
