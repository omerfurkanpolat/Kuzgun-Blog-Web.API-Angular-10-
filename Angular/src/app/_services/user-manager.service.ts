import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
    providedIn:"root"
})

export class UserManagerService{

    baseUrl:string="https://localhost:44386/api/admin"; 
    
    constructor(private http:HttpClient){

    }

    getUsers():Observable<User[]>{
       return this.http.get<User[]>(this.baseUrl+'/getusers/')

    }

    getUserDetail(id:number):Observable<User>{
        return this.http.get<User>(this.baseUrl+ '/getuserdetail/'+id)
    }

    deleteUser(id:number){    
        return this.http.delete(this.baseUrl +'/deleteUser/'+id);
    }

    
    
}
