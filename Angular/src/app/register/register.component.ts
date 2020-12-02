import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
model:any={};
  constructor(private authService:AuthService, private router:Router,private alertify:AlertifyService) { }

  ngOnInit(): void {
  }
  register() {
    this.authService.register(this.model).subscribe(()=> {
      this.alertify.success("Hesabınıza gelen maili onaylayınız");
      this.router.navigate(['/home']);
    }, error => {
      this.alertify.error(error);
    });
  }
}
