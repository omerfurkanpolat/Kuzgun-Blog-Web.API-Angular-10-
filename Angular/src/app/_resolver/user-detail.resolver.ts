import { Route } from '@angular/compiler/src/core';
import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { User } from '../_models/user';
import { AlertifyService } from '../_services/alertifyjs';
import { UserManagerService } from '../_services/user-manager.service';


@Injectable()

export class UserDetailResolver implements Resolve<User>{

    constructor(private alertifyService:AlertifyService,
        private userManagerService:UserManagerService,
        private route:Router){

    }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): User | Observable<User> | Promise<User> {
        return this.userManagerService.getUserDetail(route.params['id'])
        .pipe(catchError(error=>{
            this.alertifyService.error("server error");
            return of(null)
        }))
    }
    
}