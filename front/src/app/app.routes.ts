import { Routes } from '@angular/router';
import { CardsComponent } from './cards/cards.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path : 'cards', component: CardsComponent},
    // { path: 'about', loadChildren: './about/about.module#AboutModule' }
];


// import { NgModule } from '@angular/core';
// import { RouterModule, Routes } from '@angular/router';
// import { CardsComponent } from './cards/cards.component';

// const routes: Routes = [
//     { path: '', redirectTo: 'home', pathMatch: 'full' },
//     { path : 'cards', component: CardsComponent},
// ];

// @NgModule({
//     imports: [RouterModule.forRoot(routes)],
//     exports: [RouterModule]
//   })
// export class AppRoutingModule { }