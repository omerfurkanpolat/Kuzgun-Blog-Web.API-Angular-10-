import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { removeEmitHelper } from 'typescript';
import { SubCategory } from '../_models/subcategory';
import { AuthService } from './auth.service';



@Injectable({
    providedIn:"root"
})

export class SubCategoryService{
    baseUrl:string="https://localhost:44386/api/admin";
    baseUrl2:string="https://localhost:44386/api/writers";
    constructor(private http:HttpClient, private authService:AuthService){}

    createSubCategory(id:number,model:any ){
        return this.http.post(this.baseUrl+ '/getcategories/'+ id +'/createSubCategory', model)

    }

    getSubCategory(id:number):Observable<SubCategory>{
        return this.http.get<SubCategory>(this.baseUrl+'/getsubcategory/'+id);
        }

    
    getSubCategoryByCategoryId(id:number):Observable<SubCategory[]>{
        return this.http.get<SubCategory[]>(this.baseUrl +'/GetCategories/'+id+'/GetSubCategories')
    }

    deleteSubCategory(id:number){
        return this.http.delete(this.baseUrl+'/deleteSubCategory/'+ id)
    }
    updateSubCategory(id:number, subcategory:SubCategory):Observable<SubCategory>{
        return this.http.put<SubCategory>(this.baseUrl+'/UpdateSubCategory/'+ id, subcategory)
    }

   
    getSubCategories():Observable<SubCategory[]>{
        return this.http.get<SubCategory[]>(this.baseUrl2+'/getsubcategories')
    }

    
}

