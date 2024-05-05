import { Component} from '@angular/core';
import { CardBentoComponent } from '../card-bento/card-bento.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CardBentoComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})

export class HomeComponent{
}
