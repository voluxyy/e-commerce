import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

// import { NgModule } from '@angular/core';
// import { RouterModule, Routes } from '@angular/router';
// import { DatabaseComponent } from './components/database/database.component';
// import { HomeComponent } from './components/home/home.component';
// import { LoginComponent } from './components/login/login.component';
// import { RegisterComponent } from './components/register/register.component';
// import { ProfilComponent } from './components/profil/profil.component';
// import { FooterComponent } from './components/footer/footer.component';
// import { HeaderComponent } from './components/header/header.component';
// import { ErrorComponent } from './components/error/error.component';
// import { ProductComponent } from './components/product/product.component';
// import { BrowserModule } from '@angular/platform-browser';
// import { HttpClientModule } from '@angular/common/http';

// @NgModule({
//     declarations: [
//         DatabaseComponent,
//         HomeComponent,
//         LoginComponent,
//         RegisterComponent,
//         ProfilComponent,
//         ProductComponent,
//         FooterComponent,
//         HeaderComponent,
//         ErrorComponent
//         ],
//   imports: [
//     BrowserModule,
//     AppRoutingModule,
//     HttpClientModule,
//     RouterModule.forRoot([
//     { path: '', component: HomeComponent },
//     { path: 'login', component: LoginComponent },
//     { path: 'register', component: RegisterComponent },
//     { path: 'product', component: ProductComponent },
//     { path: 'profil', component: ProfilComponent },
//     { path: 'database', component: DatabaseComponent },
//     { path: 'footer', component: FooterComponent },
//     { path: 'header', component: HeaderComponent },
//     { path: '**', component: ErrorComponent }
//     ])
// ],
//   exports: [RouterModule]
// })
// export class AppRoutingModule { }