import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';

import { Post } from '../_models/post';
import { PostArray } from '../_models/post.array';
import { PostComment } from '../_models/postcomment';
import { AuthService } from './auth.service';

@Injectable({
    providedIn:"root"
})

export class PostService{
    baserUrl:string="https://localhost:44386/api/posts";
    baseUrl2:string="https://localhost:44386/api/writers";
    constructor(private http:HttpClient, private authService:AuthService){

    }

    getLastPost():Observable<Post>{
        return this.http.get<Post>(this.baserUrl+'/getLastPost')
    }

    getPostByEverthing(id:number):Observable<Post>{
        return this.http.get<Post>(this.baserUrl+'/getpostswitheverything/'+id)
    }

    getPostsByCategoryId(id:number):Observable<Post[]>{
        return this.http.get<Post[]>(this.baserUrl +'/getpostsbycategory/'+id)
    }

    getPostComments(id:number):Observable<PostComment[]>{
        return this.http.get<PostComment[]>(this.baserUrl +'/GetCommentByPostId/'+ id)

    }

    getAllPost():Observable<Post[]>{
        return this.http.get<Post[]>(this.baserUrl+'/getallpost');
    } 

    getPostByUser(id:number):Observable<Post[]>{
        return this.http.get<Post[]>(this.baserUrl+'/getpostbyuser/'+ id);
    }

    postAdd(id:number,model:any){
        return this.http.post(this.baseUrl2+'/addpost/'+ id, model)
    }
    getPostByUserId(id:number, postId:number):Observable<Post>{
        return this.http.get<Post>(this.baserUrl+ '/getPostByUserId/' +id + '/' + postId)
    }

    postUpdate( role:string,post:Post){
        return this.http.put(this.baseUrl2 +'/updatePost/'+role,post)

    }
    postDelete(id:number){
      return  this.http.delete(this.baseUrl2+ '/deletePost/'+id)
    }

    getLastPostOfCategories():Observable<PostArray>{
        return this.http.get<PostArray>(this.baserUrl +'/getCategoriesLastPost')
    }

    getPost(postId:number):Observable<Post>{
        return this.http.get<Post>(this.baserUrl + '/getPost/'+postId);

    }
    getPostBySubCategoryId(id:number):Observable<Post[]>{
        return this.http.get<Post[]>(this.baserUrl+'/getPostsBySubCategory/'+id)
    }

 
    
}