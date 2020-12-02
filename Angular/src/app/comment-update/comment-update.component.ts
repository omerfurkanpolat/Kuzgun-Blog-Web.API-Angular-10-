import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PostComment } from '../_models/postcomment';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { CommentService } from '../_services/comment.service';

@Component({
  selector: 'app-comment-update',
  templateUrl: './comment-update.component.html',
  styleUrls: ['./comment-update.component.css']
})
export class CommentUpdateComponent implements OnInit {
  model:any={};
  postcomment:PostComment;
  
  @Input() recipientId:number
  @Input() commentId:number
  @Input() commentt:string
  
  
  constructor(private commetService:CommentService,
     private authService:AuthService,
     private alertifyService:AlertifyService,
     private route:ActivatedRoute,
     private activeModal:NgbActiveModal,
     private router:Router
     
   ) { }

  ngOnInit(): void {
   
    this.getComment();
    
   
}

getComment(){
  this.commetService.getComment(this.commentId).subscribe(postcomment=>{
    this.postcomment=postcomment;
  })
}

updateComment(){
  this.commetService.updateComment(this.commentId, this.authService.decodedToken.nameid,this.postcomment).subscribe(()=>{
    this.alertifyService.success("Yorumunuz değiştirildi");
    this.activeModal.close();
    this.router.navigateByUrl('/home', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/post/'+ this.recipientId]);
  });
  
  }, error=>{
    this.alertifyService.error(error);
  })
}

closeModal(){
  this.activeModal.close()
}


  







}