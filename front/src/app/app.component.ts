import { CommonModule} from '@angular/common';
import { Component} from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './Components/header/header.component';
import { CardBentoComponent } from './card-bento/card-bento.component';
import { HomeComponent } from './home/home.component';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, HttpClientModule, RouterLinkActive, HomeComponent, HeaderComponent, CardBentoComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})

export class AppComponent {
  title = 'Pixel';
}
