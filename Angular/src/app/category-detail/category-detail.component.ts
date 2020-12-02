import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { allowedNodeEnvironmentFlags } from 'process';
import { Category } from '../_models/categories';
import { SubCategory } from '../_models/subcategory';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { SubCategoryService } from '../_services/subCategory.service';

@Component({
  selector: 'app-category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.css']
})
export class CategoryDetailComponent implements OnInit {


  category:Category;
  model:any={};
  subCategories:SubCategory[];
  cid:number;


  constructor(private authService:AuthService,
    private route:ActivatedRoute,
    private subCategoryService:SubCategoryService,
    private alertifyService:AlertifyService,
    private router:Router) { }

  ngOnInit(): void {
    this.route.data.subscribe(data=>{
      this.category=data.category;
    }),

    this.getSubCategoryByCategoryId()
    
    
  }

  isAdmin(){
    return this.authService.isAdmin();
  }

   getSubCategoryByCategoryId(){
     
     this.subCategoryService.getSubCategoryByCategoryId(this.route.snapshot.params['id']).subscribe(subCategories=>{
      this.subCategories=subCategories;
     })
   }

   deleteSubCategory(id:number){
    
     this.subCategoryService.deleteSubCategory(id).subscribe(()=>{
       this.alertifyService.success("Alt kategori silindi");
       this.router.navigateByUrl('/categories/categorydetail', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/categories/categorydetail/'+ this.route.snapshot.params['id']]);
    });              
        
     }, error=>{
       this.alertifyService.error(error);
     })
   }



}
