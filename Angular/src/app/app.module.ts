import { BrowserModule } from '@angular/platform-browser';
import { forwardRef, NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { JwtModule } from "@auth0/angular-jwt";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import {  HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, NgForm, NG_VALUE_ACCESSOR } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { NotfoundComponent } from './notfound/notfound.component';

import { appRoutes } from './routes';
import { ForgotpasswordComponent } from './forgotpassword/forgotpassword.component';
import { AuthGuard } from './_guard/auth-guard';
import { ErrorInterceptor } from './_services/error.interceptor';
import { MyaccountComponent } from './myaccount/myaccount.component';
import { MyAccountResolver } from './_resolver/myacount.resolver';
import { UserManageComponent } from './user-manage/user-manage.component';
import { AdminOptionsComponent } from './admin-options/admin-options.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { UserDetailResolver } from './_resolver/user-detail.resolver';
import { RolesComponent } from './roles/roles.component';
import { RoleAddComponent } from './role-add/role-add.component';
import { CategoriesComponent } from './categories/categories.component';
import { CategoryAddComponent } from './category-add/category-add.component';
import { CategoryUpdateComponent } from './category-update/category-update.component';
import { CategoryUpdateResolver } from './_resolver/category-update.resolver';
import { SubcategoryAddComponent } from './subcategory-add/subcategory-add.component';
import { SubcategoryComponent } from './subcategory/subcategory.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';
import { SubCategoryResolver } from './_resolver/subcategory.resolver';
import { FooterComponent } from './footer/footer.component';

import { MessageSendComponent } from './message-send/message-send.component';
import { MessageReadComponent } from './message-read/message-read.component';
import { MessageDetailComponent } from './message-detail/message-detail.component';
import { MessageResolver } from './_resolver/message.resolver';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { PostComponent } from './post/post.component';
import { CategoryComponent } from './category/category.component';
import { PostCategoryResolver } from './_resolver/post-category.resolver';
import { PostCommentResolver } from './_resolver/post-comment.resolver';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CommentUpdateComponent } from './comment-update/comment-update.component';
import { MypostsComponent } from './myposts/myposts.component';
import { PostAddComponent } from './post-add/post-add.component';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { PostUpdateComponent } from './post-update/post-update.component';
import { PostUpdateResolver } from './_resolver/post-update.resolver';
import { LastPostResolver } from './_resolver/last-post.resolve';
import { GetPostResolver } from './_resolver/get-post.resolver';
import { LastPostCategoriesComponent } from './last-post-categories/last-post-categories.component';
import { LastPostCategoriesResolver } from './_resolver/last-post-categories.resolver';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { registerLocaleData } from '@angular/common';
import localeTr from '@angular/common/locales/tr';
import { SubCategoryComponent } from './sub-category/sub-category.component';
import { PostSubCategoryResolver } from './_resolver/post-subcategory.resolver';
import { GetAllPostResolver } from './_resolver/get-all-post.resolver';

registerLocaleData(localeTr);






export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    RegisterComponent,
    LoginComponent,
    HomeComponent,
    NotfoundComponent,
    ForgotpasswordComponent,
    MyaccountComponent,
    UserManageComponent,
    AdminOptionsComponent,
    UserDetailComponent,
    RolesComponent,
    RoleAddComponent,
    CategoriesComponent,
    CategoryAddComponent,
    CategoryUpdateComponent,
    SubcategoryAddComponent,
    SubcategoryComponent,
    CategoryDetailComponent,
    FooterComponent,
    
    MessageSendComponent,
    MessageReadComponent,
    MessageDetailComponent,
    ConfirmEmailComponent,
    PostComponent,
    CategoryComponent,
    CommentUpdateComponent,
    MypostsComponent,
    PostAddComponent,
    PostUpdateComponent,
    LastPostCategoriesComponent,
    ResetPasswordComponent,
    SubCategoryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    AngularEditorModule ,
    
    RouterModule.forRoot(appRoutes
      ),
      JwtModule.forRoot({
        config: {
          tokenGetter: tokenGetter,
          allowedDomains: ["https://localhost:44386/"],
          disallowedRoutes: ["https://localhost:44386/api/auth"],
        },
      }),
      NgbModule,
      
  ],
  providers: [AuthGuard, 
   
    {
    provide:HTTP_INTERCEPTORS,
    useClass:ErrorInterceptor,
    multi:true
  },
  
  MyAccountResolver,
  UserDetailResolver,
  CategoryUpdateResolver,
  SubCategoryResolver,
  MessageResolver,
  PostCategoryResolver,
  PostCommentResolver,
  PostUpdateResolver,
  LastPostResolver,
  GetPostResolver,
  LastPostCategoriesResolver,
  PostSubCategoryResolver,
  GetAllPostResolver
 
  
  
],
  bootstrap: [AppComponent]
})
export class AppModule { }
