import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Category } from '../_models/categories';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { CategoryService } from '../_services/category.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  categories:Category[];
  constructor(private authService:AuthService,
    private categoryService:CategoryService,
    private alertify:AlertifyService,
    private route:Router) { }

  ngOnInit(): void {
    this.getCategories();
  }

  isAdmin(){
    return this.authService.isAdmin();
  }

  getCategories(){
    this.categoryService.getCategories().subscribe(categories=>{
      this.categories=categories;
    })
  }
  deleteCategory(id:number){
    this.categoryService.deleteCategory(id).subscribe(()=>{
      this.alertify.success("Kategory Silindi");
      this.route.navigate(['/categories']);
    }, error=>{
      this.alertify.error(error);
    })

  }

}
