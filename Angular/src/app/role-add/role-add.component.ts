import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { RoleService } from '../_services/role.service';

@Component({
  selector: 'app-role-add',
  templateUrl: './role-add.component.html',
  styleUrls: ['./role-add.component.css']
})
export class RoleAddComponent implements OnInit {
  model:any={};
  constructor(private authService:AuthService,
    private roleService:RoleService,
    private alertify:AlertifyService,
    private router:Router) { }

  ngOnInit(): void {
  }

  isAdmin(){
    return this.authService.isAdmin();
  }
  addRole(){    
    this.roleService.addRole(this.model).subscribe(()=>{
      this.alertify.success("Role Eklendi");
      this.router.navigate(['/roles']);
    }, error=>{
      this.alertify.error(error);
    }
    );
  }

}
