import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Post } from '../_models/post';

@Component({
  selector: 'app-sub-category',
  templateUrl: './sub-category.component.html',
  styleUrls: ['./sub-category.component.css']
})
export class SubCategoryComponent implements OnInit {
  posts:Post[];
  page = 1;
  pageSize =10;
  constructor( private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.route.data.subscribe(data=>{
      this.posts=data.posts;
    })
  }

}
