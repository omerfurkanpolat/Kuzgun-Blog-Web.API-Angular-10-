import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { Category } from '../_models/categories';

@Injectable({
    providedIn:"root"
})

export class CategoryService{
    baseUrl:string="https://localhost:44386/api/admin"; 
   constructor(private http:HttpClient){

   }
   

   getCategories():Observable<Category[]>{
       return this.http.get<Category[]>(this.baseUrl+'/getCategories');

   }
   deleteCategory(id:number){
       return this.http.delete(this.baseUrl+'/deleteCategory/'+ id);
   }
   addCategory(model:any){
      return this.http.post(this.baseUrl+ '/createCategory', model);
   }

   getCategory(id:number):Observable<Category>{
       return this.http.get<Category>(this.baseUrl+'/getCategory/'+id);
   }

   updateCategory(id:number, category:Category){
        return this.http.put(this.baseUrl+'/updateCategory/'+id, category)
   }
}