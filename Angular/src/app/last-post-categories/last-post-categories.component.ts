import { asNativeElements, Component, ElementRef, Input, OnInit } from '@angular/core';
import { element } from 'protractor';
import { Post } from '../_models/post';

@Component({
  selector: 'app-last-post-categories',
  templateUrl: './last-post-categories.component.html',
  styleUrls: ['./last-post-categories.component.css']
})
export class LastPostCategoriesComponent implements OnInit {
 
   @Input() post:Post
  
  constructor() { }

  ngOnInit(): void {

    
  }



}
