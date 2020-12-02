import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Messagge } from '../_models/messagge';
import { AlertifyService } from '../_services/alertifyjs';
import { MessageService } from '../_services/message.service';

@Injectable()

export class MessageResolver implements Resolve<Messagge>{
    constructor(private messageService:MessageService,
        private route:Router,
        private alertifyService:AlertifyService){}
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Messagge | Observable<Messagge> | Promise<Messagge> {
        return this.messageService.getMessage(route.params['id'])
        .pipe(catchError(error=>{
            this.alertifyService.error("Server Error");
            return of(null);
        }))
    }
    
}