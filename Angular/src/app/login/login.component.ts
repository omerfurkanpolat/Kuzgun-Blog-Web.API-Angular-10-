import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model:any={}
  constructor(private authService:AuthService, 
    private router:Router,
     private alertify:AlertifyService) { }

  ngOnInit(): void {
  }
  login() {
    this.authService.login(this.model).subscribe(next => {      
      this.router.navigate(['/home']);
      console.log(this.authService.decodedToken);
    }, error => {
      this.alertify.error(error);
    })
  }
}
