import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css']
})
export class ConfirmEmailComponent implements OnInit {

  urlParams:any={};
  emailConfirm:boolean=true;
  constructor(private authSerice:AuthService,
    private route:ActivatedRoute,
    private router:Router,
    private alertifyService:AlertifyService) { }

  ngOnInit(): void {
    this.urlParams.userId=this.route.snapshot.queryParamMap.get('UserId');
    this.urlParams.code=this.route.snapshot.queryParamMap.get('Code');
    this.confirmEmail();
  }

  confirmEmail(){  
    
     this.authSerice.confirmEmail(this.urlParams).subscribe(()=>{
       this.alertifyService.success("Email Adresiniz Onaylandı");
       this.router.navigate['/login'];
       this.emailConfirm;
     }, error=>{
       this.alertifyService.error(error);
       this.emailConfirm=false;

     })
  }

}
