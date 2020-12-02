import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Post } from '../_models/post';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { PostService } from '../_services/post.service';

@Injectable()

export class PostUpdateResolver implements Resolve<Post>{

    constructor(private postService: PostService,
        private authService: AuthService, private route: Router,
        private alertifyService: AlertifyService) {

    }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Post | Observable<Post> | Promise<Post> {
        if (this.authService.decodedToken.role == "admin") {
            return this.postService.getPost(  route.params['id'])
                .pipe(catchError(error => {
                    this.alertifyService.error("Server Error");
                    return of(null)
                }))

        }
        if (this.authService.decodedToken.role == "writer") {
            return this.postService.getPostByUserId(this.authService.decodedToken.nameid, route.params['id'])
                .pipe(catchError(error => {
                    this.alertifyService.error("Server Error");
                    return of(null)
                }))

        }

    }

} 