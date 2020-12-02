import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { faUser } from '@fortawesome/free-solid-svg-icons';
import { CategoryService } from '../_services/category.service';
import { Category } from '../_models/categories';
import { PostService } from '../_services/post.service';
import { Post } from '../_models/post';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  model: any = {};
  categories:Category[];
  posts:Post[];
  
  constructor(public authService: AuthService, 
    private router: Router, 
    private alertifyService: AlertifyService,
    private categoryService:CategoryService,
    private route:ActivatedRoute,
    private postService:PostService) { }

  ngOnInit(): void {
    this.getCategories();
  }


  loggedIn() {
    
    return this.authService.loggedIn();
   

  }

  logout() {
    localStorage.removeItem("token");
    this.alertifyService.success("Çıkış Yapıldı")
    console.log("logout");
    this.router.navigate(['/home']);
  }

  isAdmin() {
    return this.authService.isAdmin();

  }

  isWriter(){
    return this.authService.isWriter();
  }

  getCategories(){
    
    this.categoryService.getCategories().subscribe(categories=>{
      this.categories=categories;
     
    })
  }

  getPostByCategoryId(id:number){
    this.postService.getPostsByCategoryId(id).subscribe(posts=>{
      this.posts=posts;
    }, error=>{
      this.alertifyService.error(error);
    })
  }

  
}
