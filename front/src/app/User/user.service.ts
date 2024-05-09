import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

export class User {
  constructor(
    public email: string,
    public password: string
  ) {}
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = '127.0.0.1:5016/User';

  constructor(private http: HttpClient) {}

  registerUser(user: User): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/add`, user)
      .pipe(
        catchError((error: any) => {
          console.error('An error occurred:', error);
          throw error; // Rethrow so that calling code can catch it and handle it
        })
      );
  }
}

