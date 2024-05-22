import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.scss'
})
export class UserProfileComponent {
  userUrl: string;
  user: any

  id: number | undefined;

  private routeSub: Subscription = new Subscription;

  constructor(private http: HttpClient, private route: ActivatedRoute) {
    this.userUrl = 'http://localhost:5016/api/User';
  }

  ngOnInit() {
    this.routeSub = this.route.params.subscribe(params => {
      this.id = +params['id'];
      this.loadUserData(this.id);
    });
  }

  loadUserData(userId: number): void {
    // Get user data
    this.http.get<any>(`${this.userUrl}/get/${userId}`)
      .subscribe(data => {
        this.user = data;
      });
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }
}
