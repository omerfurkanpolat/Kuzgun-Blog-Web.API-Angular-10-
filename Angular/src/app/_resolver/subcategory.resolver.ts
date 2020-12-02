import {  Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { SubCategory } from '../_models/subcategory';
import { AlertifyService } from '../_services/alertifyjs';
import { SubCategoryService } from '../_services/subCategory.service';

@Injectable()

export class SubCategoryResolver implements Resolve<SubCategory>{

    constructor(private subCategoryService:SubCategoryService,
        private alertifyService:AlertifyService,
        private route:Router){

        }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): SubCategory | Observable<SubCategory> | Promise<SubCategory> {
         return this.subCategoryService.getSubCategory(route.params['id'])
         .pipe(catchError(error=>{
             this.alertifyService.error("server error");
             return of(null);
         }))
    }
    
}