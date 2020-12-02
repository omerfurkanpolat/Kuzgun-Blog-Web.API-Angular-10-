import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';


import { User } from '../_models/user';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from '../_services/alertifyjs';

@Component({
  selector: 'app-myaccount',
  templateUrl: './myaccount.component.html',
  styleUrls: ['./myaccount.component.css']
})
export class MyaccountComponent implements OnInit {
  user: User;
  model:any={};
  
  constructor(public authService:AuthService, 
   
    private alertify:AlertifyService,
    private route:ActivatedRoute,
    private router:Router) { }

  ngOnInit(): void {
    this.route.data.subscribe(data=>{
      this.user=data.user;
    })
    
  }

  changeEmail(){
    this.authService.changeEmail(this.authService.decodedToken.nameid, this.user).subscribe(()=>{
      this.alertify.success("Email Adresiniz Değiştirildi");
      this.router.navigateByUrl('/home', { skipLocationChange: true }).then(() => {
        this.router.navigate(['myaccount/'+this.route.snapshot.params['id']]);
    });
    }, error=>{
      this.alertify.error(error);
    })
  }
  changePassword(){
    this.authService.changePassword(this.authService.decodedToken.nameid, this.model)
    .subscribe(()=>{ 
      this.alertify.success("Şifreniz başarıyla değiştirildi");
      this.router.navigateByUrl('/home', { skipLocationChange: true }).then(() => {
        this.router.navigate(['myaccount/'+this.route.snapshot.params['id']]);
    });
    }, error=>{
      this.alertify.error(error);
    });

  }
  changeProfilePicture(){
    this.authService.changeProfileImage (this.authService.decodedToken.nameid, this.user)
    .subscribe(()=>{ 
      this.alertify.success("Profil resminiz başarıyla değiştirildi");
      this.router.navigateByUrl('/home', { skipLocationChange: true }).then(() => {
        this.router.navigate(['myaccount/'+this.route.snapshot.params['id']]);
    });
    }, error=>{
      this.alertify.error(error);
    });

  }



}
