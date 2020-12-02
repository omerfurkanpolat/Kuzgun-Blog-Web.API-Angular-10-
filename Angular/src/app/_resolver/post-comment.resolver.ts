import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PostComment } from '../_models/postcomment';
import { AlertifyService } from '../_services/alertifyjs';
import { PostService } from '../_services/post.service';

@Injectable()

export class PostCommentResolver implements Resolve<PostComment[]>{
    constructor(private postService:PostService,
        private route:Router,
        private alertifyService:AlertifyService){}
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): PostComment[] | Observable<PostComment[]> | Promise<PostComment[]> {
        return this.postService.getPostComments(route.params['id'])
        .pipe(catchError(error=>{
            this.alertifyService.error("Server Error")
            return of(null);
        }))
    }
    
 
}