import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";

import { map} from 'rxjs/operators';
import { JwtHelperService } from "@auth0/angular-jwt";
import { NgIf } from '@angular/common';

import { User } from '../_models/user';
import { Observable } from 'rxjs';



@Injectable({
    providedIn:"root"
})

export class AuthService{
    baseUrl:string="https://localhost:44386/api/auth/";
    jwtHelper=new JwtHelperService();
    decodedToken:any;

    constructor(private http:HttpClient) {
     
    }

    login(model: any) {
      return this.http.post(this.baseUrl+'login',model).pipe(
        map((response:any) => {
          const result = response;
          if(result) {
            localStorage.setItem("token", result.token);  
            this.decodedToken =this.jwtHelper.decodeToken(result.token);
            
          }
        })
      )
    }

    register(model:any){
      return this.http.post(this.baseUrl +'register', model)
    }
    forgotpassword(model:any){
      return this.http.post(this.baseUrl +'forgotpassword', model)
    }

    loggedIn(){
      const token=localStorage.getItem("token");
      return !this.jwtHelper.isTokenExpired(token);
      
    }

    isAdmin(){
      const token=localStorage.getItem("token");
      this.decodedToken=this.jwtHelper.decodeToken(token);
      if(this.decodedToken.role=="admin") 
      {
        return true;
      }   
    }
    isWriter(){
      const token=localStorage.getItem("token");
      this.decodedToken=this.jwtHelper.decodeToken(token);
      if(this.decodedToken.role=="writer" || this.decodedToken.role=="admin") 
      {
        return true;
      } 

    }
    confirmEmail(urlParams:any){
      return this.http.post(this.baseUrl+ 'ConfirmEmail', urlParams)
    }

    isOwnAccount(id:number){
      
      const token=localStorage.getItem("token");
      this.decodedToken=this.jwtHelper.decodeToken(token);
      if(this.decodedToken.nameid==id)
      {
        return true;
      }

    }

    resetPassword(model:any){
      return this.http.post(this.baseUrl+'resetPassword',model)

    }

    getEmail(id:number):Observable<User>{
      return this.http.get<User>(this.baseUrl+'/changeemail/'+id)

  }

  changeEmail(id:number, user:User){
      return this.http.put(this.baseUrl+'/changeemail/'+ id, user)
  }

  changePassword(id:number, model:any){
      return this.http.put(this.baseUrl+'/changepassword/'+id, model)
  }
  changeProfileImage(id:number,model:any){
    return this.http.put(this.baseUrl +'/changeProfilePicture/'+id,model)
  }

}