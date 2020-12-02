import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { SubCategoryService } from '../_services/subCategory.service';

@Component({
  selector: 'app-subcategory-add',
  templateUrl: './subcategory-add.component.html',
  styleUrls: ['./subcategory-add.component.css']
})
export class SubcategoryAddComponent implements OnInit {

  constructor(private authService:AuthService,
    private subcategoryService:SubCategoryService,
    private route:ActivatedRoute,
    private alertifyService:AlertifyService,
    private router:Router) { }
    model:any={};
  ngOnInit(): void {
  }
  isAdmin(){
    return this.authService.isAdmin();
  }

  subCategoryAdd(){
  
    this.subcategoryService.createSubCategory(this.route.snapshot.params['id'],this.model).subscribe(()=>{
      this.alertifyService.success("Alt Kategori Eklendi");
      this.router.navigate(['/categories/categorydetail/'+ this.route.snapshot.params['id']]);
    }, error=>{
      this.alertifyService.error(error);
    });

  }

}
