import { CommonModule} from '@angular/common';
import { Component, Input} from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './Components/header/header.component';
import { CardBentoComponent } from './card-bento/card-bento.component';
import { HomeComponent } from './home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { AddProductComponent } from './Admin/add-product/add-product.component';
import { EditProductComponent } from './Admin/edit-product/edit-product.component';
import { GamesAdminComponent } from './Admin/games-admin/games-admin.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule, 
    RouterOutlet, 
    RouterLink, 
    HttpClientModule, 
    RouterLinkActive,
    HomeComponent, 
    HeaderComponent, 
    CardBentoComponent,
    GamesAdminComponent,
    EditProductComponent, 
    ShoppingCartComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})

export class AppComponent {
  title = 'Pixel';
}
