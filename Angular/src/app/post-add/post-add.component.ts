import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Category } from '../_models/categories';
import { SubCategory } from '../_models/subcategory';
import { User } from '../_models/user';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { CategoryService } from '../_services/category.service';
import { PostService } from '../_services/post.service';
import { SubCategoryService } from '../_services/subCategory.service';


@Component({
  selector: 'app-post-add',
  templateUrl: './post-add.component.html',
  styleUrls: ['./post-add.component.css']
})
export class PostAddComponent implements OnInit {

  model:any={};
  subCategories:SubCategory[];
  categories:Category[];
  


  constructor(private subCategoryService:SubCategoryService, 
    private postService:PostService,
    public authService:AuthService,
    private alertifyService:AlertifyService,
    private route:Router,
    private categoryService:CategoryService
    ) { }

  ngOnInit(): void {
    
    this.getCategories();
    
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

  postAdd(){
    this.postService.postAdd(this.authService.decodedToken.nameid, this.model).subscribe(()=>{
      this.alertifyService.success("Makaleniz Başarıyla Eklendi");
      this.route.navigate(['/mypost']);
    }, error=>{
      this.alertifyService.error(error);
    })
  }
}
