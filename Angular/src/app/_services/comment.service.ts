import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { PostComment } from '../_models/postcomment';

@Injectable({providedIn:'root'})

export class CommentService{
    baseUrl:string="https://localhost:44386/api/users"; 

    constructor(private http:HttpClient){}


    addComment(id:number, userId:number,model:any){
        return this.http.post(this.baseUrl+'/addcomment/'+id+'/'+ userId,model)
    }
    deleteComment(id:number){
        return this.http.delete(this.baseUrl+'/deletecomment/'+id)
    }

   updateComment(id:number ,userId:number , postcomment:PostComment){
       return this.http.put(this.baseUrl+'/updatecomment/'+id+'/'+userId, postcomment);

   }
   getComment(id:number):Observable<PostComment>{
       return this.http.get<PostComment>(this.baseUrl +'/getComment/'+id);
   }

   commentExists(userId:number, postId ):Observable<PostComment>{
       return this.http.get<PostComment>(this.baseUrl+'/commentExists/'+userId + '/' + postId)
   }
}