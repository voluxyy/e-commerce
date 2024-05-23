import { NgFor } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-edit-member',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './edit-member.component.html',
  styleUrl: './edit-member.component.scss'
})
export class EditMemberComponent {
  categories: any;
  userUrl: string;
  form: FormGroup;
  user: any;

  private routeSub: Subscription = new Subscription;
  private id: number | undefined;

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) {
    this.userUrl = 'http://localhost:5016/api/User';
    this.form = this.fb.group({
      id: new FormControl<number | null>(null),
      lastname: new FormControl<string | null>(null),
      firstname: new FormControl<string | null>(null),
      pseudo: new FormControl<string | null>(null),
      email: new FormControl<string | null>(null),
      birthdate: new FormControl<string | null>(null),
      money: new FormControl<number | null>(null),
    });
  }

  ngOnInit() {
    this.routeSub = this.route.params.subscribe(async params => {
      this.id = +params['id'];
      await this.loadProductData(this.id);
    });
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
        lastname: user.lastname,
        firstname: user.firstname,
        pseudo: user.pseudo,
        email: user.email,
        birthdate: user.birthdate,
        money: user.money,
      });
    } catch (error) {
      console.error('An error occurred:', error);
    }
  }

  onSubmit() {
    const dto = {
      id: this.form.value.id,
      lastname: this.form.value.lastname,
      firstname: this.form.value.firstname,
      pseudo: this.form.value.pseudo,
      email: this.user.email,
      birthdate: this.user.birthdate,
      money: this.form.value.money,
    };

    const formData = JSON.stringify(dto);
    const headers = new HttpHeaders().set("Content-Type", "application/json");

    this.http.put<any>(`${this.userUrl}/update/${this.id}`, formData, { headers })
      .subscribe(data => {
        window.location.href = "membersAdmin";
      }, error => {
        console.log(error);
      });
  }
}
