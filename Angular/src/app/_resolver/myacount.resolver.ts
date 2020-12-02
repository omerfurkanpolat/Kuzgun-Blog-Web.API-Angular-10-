import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { User } from '../_models/user';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class MyAccountResolver implements Resolve<User>{

    constructor(public authService:AuthService,   
        private alertify:AlertifyService,
        private route:Router){

    }
    
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): User | Observable<User> | Promise<User> {
         return this.authService.getEmail(this.authService.decodedToken.nameid)
        .pipe(catchError(error=>{
            this.alertify.error("server error");
            this.route.navigate(['/home']); 
            return of(null)
        }))
    }
    

}