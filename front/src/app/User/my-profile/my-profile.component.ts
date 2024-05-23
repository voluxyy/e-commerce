import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-my-profile',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './my-profile.component.html',
  styleUrl: './my-profile.component.scss'
})
export class MyProfileComponent {
  userUrl: string;
  user: any

  id: string | undefined;

  private routeSub: Subscription = new Subscription;

  constructor(private http: HttpClient, private cookie: CookieService) {
    this.userUrl = 'http://localhost:5016/api/User';
  }

  ngOnInit() {
    this.id = this.cookie.get("UserId");
    if (!this.id) {
      window.location.href = "";
      return;
    }

    this.loadUserData(this.id)
  }

  async loadUserData(userId: string): Promise<void> {
    // Get user data
    await this.http.get<any>(`${this.userUrl}/get/${userId}`)
      .subscribe(data => {
        this.user = data;
      });
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }
}
