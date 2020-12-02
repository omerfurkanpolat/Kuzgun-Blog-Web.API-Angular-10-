import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SubCategory } from '../_models/subcategory';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { SubCategoryService } from '../_services/subCategory.service';

@Component({
  selector: 'app-subcategory',
  templateUrl: './subcategory.component.html',
  styleUrls: ['./subcategory.component.css']
})
export class SubcategoryComponent implements OnInit {
  subcategory:SubCategory;
  model:{};
  
  
  constructor(private authService:AuthService,
    private route:ActivatedRoute,
    private subCategoryService:SubCategoryService,
    private alertifyService:AlertifyService,
    private router:Router) { }
  

  ngOnInit(): void {
    this.route.data.subscribe(data=>{
      this.subcategory=data.subcategory;
    })
  }
  isAdmin(){
    return this.authService.isAdmin();
  }

  updateSubCategory(){
    this.subCategoryService.updateSubCategory(this.route.snapshot.params['id'], this.subcategory).subscribe(()=>{
      this.alertifyService.success("Alt Kategori Güncellendi");      
       this.router.navigate(['/categories']);
    }, error=>{
      this.alertifyService.error(error);
    })
  }
}
