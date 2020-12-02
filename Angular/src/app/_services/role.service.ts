import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { Role } from '../_models/role';


@Injectable({
    providedIn:"root"
})
 
export class RoleService{

    baseUrl:string="https://localhost:44386/api/admin";
    constructor(private http:HttpClient){         

    }

    getRoles():Observable<Role[]>{
       return this.http.get<Role[]>(this.baseUrl+'/getroles');
    }
    addRole(model:any){
       return this.http.post(this.baseUrl+'/addrole', model);
    }

    deleteRole(id:number){
        return this.http.delete(this.baseUrl +'/deleteRole/'+ id);
    }
    changeUserRole(id:number,model:any ){
        return this.http.put(this.baseUrl+ '/changeUserRole/'+ id , model)

    }
}