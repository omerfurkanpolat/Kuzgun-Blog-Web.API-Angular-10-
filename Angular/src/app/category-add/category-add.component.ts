import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { CategoryService } from '../_services/category.service';

@Component({
  selector: 'app-category-add',
  templateUrl: './category-add.component.html',
  styleUrls: ['./category-add.component.css']
})
export class CategoryAddComponent implements OnInit {
  model:any={};
  constructor(private authService:AuthService,
    private categoryService:CategoryService,
    private alertify:AlertifyService,
    private route:Router) { }

  ngOnInit(): void {
  }

  isAdmin(){
    return this.authService.isAdmin();
  }

  addCategory(){
    this.categoryService.addCategory(this.model).subscribe(()=>{
      this.alertify.success("Kategori Eklendi");
      this.route.navigate(['/categories'])
    }, error=>{
      this.alertify.error(error);
    })
  }

}
