
import { Routes } from '@angular/router';

import { CategoriesComponent } from './categories/categories.component';
import { CategoryAddComponent } from './category-add/category-add.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';
import { CategoryUpdateComponent } from './category-update/category-update.component';
import { CategoryComponent } from './category/category.component';
import { CommentUpdateComponent } from './comment-update/comment-update.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { ForgotpasswordComponent } from './forgotpassword/forgotpassword.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { MessageDetailComponent } from './message-detail/message-detail.component';
import { MessageReadComponent } from './message-read/message-read.component';
import { MessageSendComponent } from './message-send/message-send.component';
import { MyaccountComponent } from './myaccount/myaccount.component';
import { MypostsComponent } from './myposts/myposts.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { PostAddComponent } from './post-add/post-add.component';
import { PostUpdateComponent } from './post-update/post-update.component';
import { PostComponent } from './post/post.component';
import { RegisterComponent } from './register/register.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { RoleAddComponent } from './role-add/role-add.component';

import { RolesComponent } from './roles/roles.component';
import { SubCategoryComponent } from './sub-category/sub-category.component';

import { SubcategoryAddComponent } from './subcategory-add/subcategory-add.component';
import { SubcategoryComponent } from './subcategory/subcategory.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { UserManageComponent } from './user-manage/user-manage.component';
import { AuthGuard } from './_guard/auth-guard';

import { CategoryUpdateResolver } from './_resolver/category-update.resolver';
import { GetAllPostResolver } from './_resolver/get-all-post.resolver';
import { GetPostResolver } from './_resolver/get-post.resolver';
import { LastPostCategoriesResolver } from './_resolver/last-post-categories.resolver';
import { LastPostResolver } from './_resolver/last-post.resolve';

import { MessageResolver } from './_resolver/message.resolver';
import { MyAccountResolver } from './_resolver/myacount.resolver';
import { PostCategoryResolver } from './_resolver/post-category.resolver';
import { PostSubCategoryResolver } from './_resolver/post-subcategory.resolver';
import { PostUpdateResolver } from './_resolver/post-update.resolver';
import { SubCategoryResolver } from './_resolver/subcategory.resolver';
import { UserDetailResolver } from './_resolver/user-detail.resolver';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent, resolve: { post: LastPostResolver, postArray: LastPostCategoriesResolver, posts: GetAllPostResolver } },
    { path: 'home', component: HomeComponent, resolve: { post: LastPostResolver, postArray: LastPostCategoriesResolver, posts: GetAllPostResolver } },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'forgotpassword', component: ForgotpasswordComponent },
    { path: 'myaccount/:id', component: MyaccountComponent, resolve: { user: MyAccountResolver }, canActivate: [AuthGuard] },
    { path: 'usermanage', component: UserManageComponent, canActivate: [AuthGuard] },
    { path: 'roles', component: RolesComponent, canActivate: [AuthGuard] },
    { path: 'roles/roleadd', component: RoleAddComponent, canActivate: [AuthGuard] },
    { path: 'categories', component: CategoriesComponent, canActivate: [AuthGuard] },
    { path: 'categories/categoryAdd', component: CategoryAddComponent, canActivate: [AuthGuard] },
    { path: 'categories/updateCategory/:id', component: CategoryUpdateComponent, resolve: { category: CategoryUpdateResolver }, canActivate: [AuthGuard] },
    { path: 'usermanage/userdetail/:id', component: UserDetailComponent, resolve: { user: UserDetailResolver }, canActivate: [AuthGuard] },
    { path: 'categories/:id/subCategoryAdd', component: SubcategoryAddComponent, canActivate: [AuthGuard] },
    { path: 'categories/categorydetail/:id', component: CategoryDetailComponent, resolve: { category: CategoryUpdateResolver }, canActivate: [AuthGuard] },
    { path: 'subcategory-update/:id', component: SubcategoryComponent, resolve: { subcategory: SubCategoryResolver }, canActivate: [AuthGuard] },
    { path: 'sendmessage', component: MessageSendComponent },
    { path: 'messageread', component: MessageReadComponent, canActivate: [AuthGuard] },
    { path: 'messageread/messagedetail/:id', component: MessageDetailComponent, resolve: { messagge: MessageResolver }, canActivate: [AuthGuard] },
    { path: 'confirmEmail', component: ConfirmEmailComponent },
    { path: 'post/:id', component: PostComponent, resolve: { comment: PostCategoryResolver, post: GetPostResolver } },
    { path: 'category/:id', component: CategoryComponent, resolve: { posts: PostCategoryResolver } },
    { path: 'mypost', component: MypostsComponent, canActivate: [AuthGuard] },
    { path: 'postadd', component: PostAddComponent, canActivate: [AuthGuard] },
    { path: 'resetpassword', component: ResetPasswordComponent },
    { path: 'postupdate/:id', component: PostUpdateComponent, resolve: { post: PostUpdateResolver }, canActivate: [AuthGuard] },
    { path: 'subcategory/:id', component: SubCategoryComponent, resolve: { posts: PostSubCategoryResolver } },
    { path: '**', component: NotfoundComponent },
    { path: 'notfound', component: NotfoundComponent },
];
