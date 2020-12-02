import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.css']
})
export class ForgotpasswordComponent implements OnInit {
  model:any={};
  
  constructor(private authService:AuthService, 
    private router:Router,
    private alertifyService:AlertifyService
    ) { }

  ngOnInit(): void {
    
  }
  forgotpassword(){
    this.authService.forgotpassword(this.model).subscribe(()=>{
      this.alertifyService.success("Parolanızı sıfırlamanız için mail gönderildi")
      this.router.navigate(['/home']);
    }, error => {
      this.alertifyService.error(error);
    });
  }
 
}
