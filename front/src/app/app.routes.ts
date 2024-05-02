import { Routes } from '@angular/router';
import { CardsComponent } from './cards/cards.component';
import { CardComponent } from './card/card.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

export const routes: Routes = [
    { path: '',   redirectTo: '/', pathMatch: 'full' },
    { 
        path: 'home',
        component: CardsComponent,
        children: [
            {
                path: 'cards',
                component: CardsComponent,
                children: [
                    { path: 'card', component: CardComponent }
                ]
            }
        
        ]},
    { path: '**', component: PageNotFoundComponent },
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