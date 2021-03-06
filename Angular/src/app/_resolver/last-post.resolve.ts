import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Post } from '../_models/post';
import { AlertifyService } from '../_services/alertifyjs';
import { PostService } from '../_services/post.service';

@Injectable()

export class LastPostResolver implements Resolve<Post>{
    constructor(private postService:PostService,
        private alertifyService:AlertifyService){

    }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Post | Observable<Post> | Promise<Post> {
        return this.postService.getLastPost()
        .pipe(catchError(error=>{
            this.alertifyService.error("Server Error")
            return of(null)
        }))
    }
    
}