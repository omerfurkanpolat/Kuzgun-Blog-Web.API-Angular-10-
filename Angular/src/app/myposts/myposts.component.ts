import { Component, OnInit } from '@angular/core';
import { Post } from '../_models/post';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { PostService } from '../_services/post.service';

@Component({
  selector: 'app-myposts',
  templateUrl: './myposts.component.html',
  styleUrls: ['./myposts.component.css']
})
export class MypostsComponent implements OnInit {

  posts:Post[];

  constructor(private postService:PostService,
    private authService:AuthService, 
    private alertifyService:AlertifyService) { }

  ngOnInit(): void {

    this.getPostByUser();
  }


  getPostByUser(){
    if(this.authService.decodedToken.role=="admin")
    {
      
      this.postService.getAllPost().subscribe(posts=>{
        this.posts=posts;
      }, error=>{
        this.alertifyService.error(error);
      })
    }
    if(this.authService.decodedToken.role=="writer")
    {
      
      this.postService.getPostByUser(this.authService.decodedToken.nameid).subscribe(posts=>{
        this.posts=posts
      }, error=>{
        this.alertifyService.error(error);
      })
    }
    
  }

  
}
