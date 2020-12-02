import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from '../_models/categories';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { CategoryService } from '../_services/category.service';

@Component({
  selector: 'app-category-update',
  templateUrl: './category-update.component.html',
  styleUrls: ['./category-update.component.css']
})
export class CategoryUpdateComponent implements OnInit {
  category:Category;
  model:any={};
  constructor(private authService:AuthService,
    private alertifyService:AlertifyService,
    private categoryService:CategoryService,
    private route:ActivatedRoute,
    private router:Router) { }

  ngOnInit(): void {
    this.route.data.subscribe(data=>{
      this.category=data.category;
    })
 
  }
  isAdmin(){
    return this.authService.isAdmin();
  }
  updateCategory(){   
    this.categoryService.updateCategory(this.route.snapshot.params['id'], this.category).subscribe(()=>{
      this.alertifyService.success("Kategori Güncellendi");                            
      this.router.navigate(['categories']);
    }, error=>{
      this.alertifyService.error(error);
    });
  }

}
