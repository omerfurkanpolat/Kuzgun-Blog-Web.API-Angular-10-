import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Category } from '../_models/categories';

import { Post } from '../_models/post';
import { PostArray } from '../_models/post.array';
import { AlertifyService } from '../_services/alertifyjs';
import { CategoryService } from '../_services/category.service';
import { PostService } from '../_services/post.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  postArray:PostArray;
  post:Post;
  posts:Post[];
  page = 1;
  pageSize =10;
 
  constructor( private postService:PostService,
    private route:ActivatedRoute
    ) { }

  ngOnInit(): void {
    this.route.data.subscribe(data=>{
      this.post=data.post;
    })
    this.route.data.subscribe(data=>{
      this.postArray=data.postArray;
    })
    this.route.data.subscribe(data=>{
      this.posts=data.posts;
    })
    
    
  }

 
  

}
