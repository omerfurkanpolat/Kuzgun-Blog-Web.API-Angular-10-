import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';


export class ErrorInterceptor implements HttpInterceptor{
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req)
            .pipe(catchError((error:HttpErrorResponse)=>{
                console.log(error);
                if(error.status===400){
                    if(error.error.modelState){
                        const serverError=error.error;
                        let errorMessage='';
                        for(const key in serverError.modelState)
                        {
                            errorMessage+=serverError.modelState[key] +'\n';
                        }
                        return throwError(errorMessage);
                    }
                    return throwError(error.error.message);
                }
            }));
    }

}