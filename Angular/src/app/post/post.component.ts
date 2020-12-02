import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommentUpdateComponent } from '../comment-update/comment-update.component';

import { Post } from '../_models/post';
import { PostComment } from '../_models/postcomment';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { CommentService } from '../_services/comment.service';
import { PostService } from '../_services/post.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  post: Post;
  postcomment: any;
  postcomments: PostComment[];
  model: any = {};
  exist: PostComment;


  constructor(private postService: PostService,
    private alertifyService: AlertifyService,
    private route: ActivatedRoute,
    public authService: AuthService,
    private commentService: CommentService,
    private router: Router, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.post = data.post;
    })

    this.getPostComments();
    this.commentExist();




  }

  getPostComments() {
    this.postService.getPostComments(this.route.snapshot.params['id']).subscribe(postcomments => {
      this.postcomments = postcomments;

    })
  }
  getPostById() {
    this.postService.getPostByEverthing(this.route.snapshot.params['id']).subscribe(post => {
      this.post = post;
    }, error => {
      this.alertifyService.error(error);
    })
  }

  isOwnAccount(id: number) {
    return this.authService.isOwnAccount(id);
  }

  reportComment() {
    this.alertifyService.success("Bildiriniz için teşekkür ederiz");
  }

  loggedIn() {

    return this.authService.loggedIn();

  }

  commentAdd(id: number) {
    this.commentService.addComment(id, this.authService.decodedToken.nameid, this.model).subscribe(() => {

      this.alertifyService.success("Yorumunuz Eklendi");
      this.router.navigateByUrl('/home', { skipLocationChange: true }).then(() => {
        this.router.navigate(['post/' + this.post.postId]);
      });

    }, error => {
      this.alertifyService.error(error);
    })
  }


  commentDelete(id: number) {
    this.commentService.deleteComment(id).subscribe(() => {
      this.alertifyService.success("Mesajınız Silindi");
      this.router.navigateByUrl('/home', { skipLocationChange: true }).then(() => {
        this.router.navigate(['post/' + this.post.postId]);
      })

    }, error => {
      this.alertifyService.error(error);
    }
    )
  }
  isAdmin() {
    return this.authService.isAdmin();
  }

  openCommentUpdateModel(id: number, comment: string) {

    const modalRef = this.modalService.open(CommentUpdateComponent);
    modalRef.componentInstance.recipientId = this.post.postId;
    modalRef.componentInstance.commentId = id;
    modalRef.componentInstance.commentt = comment;

  }

  commentExist() {
    this.commentService.commentExists(this.authService.decodedToken.nameid, this.post.postId).subscribe(exists => {
      this.exist = exists
     
    })


  }
}




