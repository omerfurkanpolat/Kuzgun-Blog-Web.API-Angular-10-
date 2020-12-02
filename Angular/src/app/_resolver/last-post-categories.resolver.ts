import {  Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PostArray } from '../_models/post.array';
import { AlertifyService } from '../_services/alertifyjs';
import { PostService } from '../_services/post.service';


@Injectable()

export class LastPostCategoriesResolver implements Resolve<PostArray>{

    constructor(private postService:PostService,
        private alertifyService:AlertifyService){}
        
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): PostArray | Observable<PostArray> | Promise<PostArray> {
        return this.postService.getLastPostOfCategories()
        .pipe(catchError(error=>{
            this.alertifyService.error("Server Error");
            return of(null)
            
        }))
    }

}
