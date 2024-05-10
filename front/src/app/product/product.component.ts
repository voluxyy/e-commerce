import { NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'product',
  standalone: true,
  imports: [RouterOutlet, NgFor, NgIf],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss'
})
export class ProductComponent implements OnInit{
  prod: any;

  constructor(private route: ActivatedRoute) { }


  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.prod = params['id'];
    });
  }
}