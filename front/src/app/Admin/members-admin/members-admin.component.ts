import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-members-admin',
  standalone: true,
  imports: [NgFor],
  templateUrl: './members-admin.component.html',
  styleUrl: './members-admin.component.scss'
})
export class MembersAdminComponent {
  users: any;
  userUrl: string;

  constructor(private http: HttpClient) {
    this.userUrl = 'http://localhost:5016/api/User';
  }


  ngOnInit(): void {
    this.http.get<any>(this.userUrl + "/all")
      .subscribe(data => {
        this.users = data;
      });
  }
}
