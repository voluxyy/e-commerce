import { Routes } from '@angular/router';
import { CardComponent } from './card/card.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { CardBentoComponent } from './card-bento/card-bento.component';
import { HomeComponent } from './home/home.component';

export const routes: Routes = [
    { path: '', redirectTo: '/', pathMatch: 'full' },
    {
        path: 'home',
        component: HomeComponent,
        title: 'Home Page',
        children: [
            {
                path: 'cards',
                component: CardBentoComponent,
                title: 'Bento Cards',
                children: [
                    { 
                        path: 'card', 
                        title: 'Product Card',
                        component: CardComponent }
                ]
            },
        ]
    },
    
    { path: '**', title: "Page Not Found", component: PageNotFoundComponent },
];