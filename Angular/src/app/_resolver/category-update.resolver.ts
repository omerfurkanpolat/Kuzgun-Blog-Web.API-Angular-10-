import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Category } from '../_models/categories';
import { AlertifyService } from '../_services/alertifyjs';
import { CategoryService } from '../_services/category.service';

@Injectable()

export class CategoryUpdateResolver implements Resolve<Category>{

    constructor(private alertify:AlertifyService,
        private categoryService:CategoryService,
        private route:Router){}
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Category | Observable<Category> | Promise<Category> {
        return this.categoryService.getCategory(route.params['id'])
        .pipe(catchError(error=>{
            this.alertify.error("server error");
            return of(null);
        }))
        
    }

}