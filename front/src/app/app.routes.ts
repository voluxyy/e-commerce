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
import { AddProductComponent } from './Admin/add-product/add-product.component';
import { EditProductComponent } from './Admin/edit-product/edit-product.component';
import { AdminLoginComponent } from './Admin/login/login.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
import { MyProfileComponent } from './User/my-profile/my-profile.component';
import { UserProfileComponent } from './User/user-profile/user-profile.component';
import { AddCategoryComponent } from './Admin/add-category/add-category.component';
import { EditCategoryComponent } from './Admin/edit-category/edit-category.component';
import { EditMemberComponent } from './Admin/edit-member/edit-member.component';
import { EditProfileComponent } from './User/edit-profile/edit-profile.component';
import { EditPasswordComponent } from './User/edit-password/edit-password.component';
import { AddReviewComponent } from './add-review/add-review.component';
import { EditReviewComponent } from './edit-review/edit-review.component';

export const routes: Routes = [
    {
        path: '',
        title: 'Home Page',
        component: HomeComponent,
        children: [
            {
                path: 'Video Games',
                component: CardBentoComponent,
                title: 'Bento Cards',
            },
        ]
    },
    { path: 'product/:id', title: 'Product', component: ProductComponent },

    // Admin routes
    { path: 'admin-login', title: 'AdminLogin', component: AdminLoginComponent},
    { path: 'membersAdmin', title: 'Members', component: MembersAdminComponent} ,
    { path: 'gamesAdmin', title: 'GamesAdmin', component: GamesAdminComponent },
    { path: 'add-product', component: AddProductComponent, title: 'Add Product' }, 
    { path: 'edit-product/:id', component: EditProductComponent, title: 'Edit Product' },
    { path: 'add-category', title: 'Add Category', component: AddCategoryComponent },
    { path: 'edit-category/:id', title: 'Edit Category', component: EditCategoryComponent},
    { path: 'edit-member/:id', title: 'Edit Member', component: EditMemberComponent},

    // User routes
    { path: 'shopping-cart', title: 'Shopping Cart', component: ShoppingCartComponent },
    { path: 'register', title: 'Register', component: RegisterComponent},
    { path: 'login', title: 'Login', component: LoginComponent},
    { path: 'dashboard', title: 'Dashboard', component: DashboardComponent},
    { path: 'my-profile', title: 'My Profile', component: MyProfileComponent},
    { path: 'user-profile/:id', title: 'User Profile', component: UserProfileComponent},
    { path: 'edit-profile', title: 'Edit profile', component: EditProfileComponent},
    { path: 'edit-password', title: 'Edit password', component: EditPasswordComponent},
    { path: 'add-review/:id', title: 'Add review', component: AddReviewComponent},
    { path: 'edit-review/:id', title: 'Edit review', component: EditReviewComponent},

    // Default route
    { path: '**', title: "Page Not Found", component: PageNotFoundComponent },
];