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
import { MyProfileComponent } from './User/my-profile/my-profile.component';
import { UserProfileComponent } from './User/user-profile/user-profile.component';
import { FooterComponent } from './Components/footer/footer.component';
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
    ShoppingCartComponent,
    MyProfileComponent,
    UserProfileComponent,
    FooterComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})

export class AppComponent {
  title = 'Pixel';
}
