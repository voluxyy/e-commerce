import { Component } from '@angular/core';
import { CardComponent } from '../card/card.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-card-bento',
  standalone: true,
  imports: [CardComponent, RouterOutlet],
  templateUrl: './card-bento.component.html',
  styleUrl: './card-bento.component.scss'
})
export class CardBentoComponent {

}
