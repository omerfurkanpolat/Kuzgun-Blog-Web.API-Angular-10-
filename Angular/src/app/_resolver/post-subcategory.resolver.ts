import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Post } from '../_models/post';
import { AlertifyService } from '../_services/alertifyjs';
import { PostService } from '../_services/post.service';

@Injectable()
export class PostSubCategoryResolver implements Resolve<Post[]>{
    constructor(private postService:PostService,
        private route:Router,
        private alertifyService:AlertifyService){

    }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Post[] | Observable<Post[]> | Promise<Post[]> {
        return this.postService.getPostBySubCategoryId(route.params['id'])
        .pipe(catchError(error=>{
             this.alertifyService.error("server error"); 
             return of(null);
         }))
    }
    
}