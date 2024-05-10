import { Routes } from '@angular/router';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { CardBentoComponent } from './card-bento/card-bento.component';
import { HomeComponent } from './home/home.component';
import { DashboardComponent } from './Admin/dashboard/dashboard.component';
import { RegisterComponent } from './User/register/register.component';
import { LoginComponent } from './User/login/login.component';
import { GamesAdminComponent } from './Admin/games-admin/games-admin.component';
import { MembersAdminComponent } from './Admin/members-admin/members-admin.component';
import { ProductComponent } from './product/product.component';

export const routes: Routes = [
    {
        path: '',
        component: HomeComponent,
        title: 'Home Page',
        children: [
            {
                path: 'Video Games',
                component: CardBentoComponent,
                title: 'Bento Cards',
            },
        ]
    },
    { path: 'product/:id', title: 'Product', component: ProductComponent},
    { path: 'membersAdmin', title: 'Members', component: MembersAdminComponent},
    { path: 'gamesAdmin', title: 'GamesAdmin', component: GamesAdminComponent},
    { path: 'register', title: 'Register', component: RegisterComponent},
    { path: 'login', title: 'Login', component: LoginComponent},
    { path: 'dashboard', title: 'Dashboard', component: DashboardComponent},
    { path: '**', title: "Page Not Found", component: PageNotFoundComponent },
];