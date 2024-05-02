import { Routes } from '@angular/router';
import { CardsComponent } from './cards/cards.component';
import { CardComponent } from './card/card.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

export const routes: Routes = [
    { path: '', redirectTo: '/', pathMatch: 'full' },
    {
        path: 'home',
        component: CardsComponent,
        title: 'Home Page',
        children: [
            {
                path: 'cards',
                component: CardsComponent,
                title: 'Bento Cards',
                children: [
                    { 
                        path: 'card', 
                        title: 'Product Card',
                        component: CardComponent }
                ]
            },
            { path: '**', title: "Page Not Found", component: PageNotFoundComponent },
        ]
    },
];