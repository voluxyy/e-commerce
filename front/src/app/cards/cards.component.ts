import { Component } from '@angular/core';
import { CardComponent } from '../card/card.component';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-cards',
  standalone: true,
  imports: [CardComponent, NgFor],
  templateUrl: './cards.component.html',
  styleUrl: './cards.component.scss'
})
export class CardsComponent {
 cards: any;

  constructor() {
    this.cards = {
      name: 'GTA V',
      price: 15 + '€',
      quantity: '10000',
      category: 'Electronics'
    };
  }
}
