import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from '../_models/categories';
import { Post } from '../_models/post';
import { SubCategory } from '../_models/subcategory';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { CategoryService } from '../_services/category.service';
import { PostService } from '../_services/post.service';
import { SubCategoryService } from '../_services/subCategory.service';

@Component({
  selector: 'app-post-update',
  templateUrl: './post-update.component.html',
  styleUrls: ['./post-update.component.css']
})
export class PostUpdateComponent implements OnInit {

  post:Post;
  subCategories:SubCategory[];
  model:any={};
  categories:Category[];
  constructor(private route:ActivatedRoute,
    private subCategoryService:SubCategoryService,
    private postService:PostService,
    private alertifyService:AlertifyService,
    private router:Router,
    private authService:AuthService,
    private categoryService:CategoryService) { }

  ngOnInit(): void {
    this.route.data.subscribe(data=>{
      this.post=data.post;
    }),
    this.getCategories();
    
  
    this.getSubCategories();
  }

  getSubCategories(){
    this.subCategoryService.getSubCategories().subscribe(subCategories=>{
      this.subCategories=subCategories;
      
    })
  }

  updatePost(){
    console.log(this.post)
    if(this.post==null)
    {
      error=>{
        this.alertifyService.error("Eksik Bilgi Girdiniz");
      }
    }
    this.postService.postUpdate(this.authService.decodedToken.role, this.post).subscribe(()=>{
      this.alertifyService.success("Makaledeki değişiklikler başarı ile kaydedildi");
      this.router.navigate(['/mypost']);
    }, error=>{
      this.alertifyService.error(error);
    })
  }

  deletePost(id:number){
     this.postService.postDelete(id).subscribe(()=>{
       this.alertifyService.success("Makale Silindi");
       this.router.navigate(['/mypost']);
      
     },
     error=>{
       this.alertifyService.error(error);
     })
  }

  getSubCategoryByCategoryId(id:number){
    this.subCategoryService.getSubCategoryByCategoryId(id).subscribe(subCategories=>{
      this.subCategories=subCategories
      
    })
  }
  getCategories(){
    this.categoryService.getCategories().subscribe(categories=>{
      this.categories=categories;
    })

  }

}
