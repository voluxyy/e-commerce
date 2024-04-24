import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [],
  templateUrl: './header.component.html',
  styleUrl: './header.component.sass'
})
export class HeaderComponent implements OnInit{
  title = 'Pixel - Buy Video Games Online';
  target: string;


  constructor(private http: HttpClient) {
    this.target = "https://127.0.0.1:7094";
  }

  ngOnInit(): void {
    this.http.get<any>(this.target+"/api/home")
    .subscribe(resp => {
      this.title = resp;
    });
  }
}
