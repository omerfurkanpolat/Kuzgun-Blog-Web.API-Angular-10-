import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  model:any={};
  urlParams:any={};
  constructor(private route:ActivatedRoute,private authService:AuthService,
    private alertifyService:AlertifyService,
    private router:Router) { }

  ngOnInit(): void {
    this.urlParams.userId=this.route.snapshot.queryParamMap.get('UserId');
    this.urlParams.code=this.route.snapshot.queryParamMap.get('Code');
    this.model.code=this.urlParams.code;
    this.model.userId=this.urlParams.userId;
   
  }

  resetPasword(){
    this.authService.resetPassword( this.model).subscribe(()=>{
      this.alertifyService.success("Şifreniz Değiştirildi");
      this.router.navigate['/login'];
    }, error=>{
      this.alertifyService.error(error);
    })
  }




}
