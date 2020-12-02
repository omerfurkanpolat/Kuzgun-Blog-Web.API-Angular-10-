import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from '../_models/categories';
import { Post } from '../_models/post';
import { AlertifyService } from '../_services/alertifyjs';
import { CategoryService } from '../_services/category.service';
import { PostService } from '../_services/post.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  posts:Post[];
  category:Category;
  page = 1;
  pageSize =10;

  constructor(private postService:PostService,
    private route:ActivatedRoute,
    private alertifyService:AlertifyService,
    private categoryService:CategoryService,
    router:Router
   ) { }

  ngOnInit(): void {
    this.route.data.subscribe(data=>{
      this.posts=data.posts;
    })
   
  
  } 





}
