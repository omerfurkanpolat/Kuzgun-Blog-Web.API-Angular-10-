import { Component, OnInit } from '@angular/core';
import { ActivationEnd, Router } from '@angular/router';
import { Role } from '../_models/role';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { RoleService } from '../_services/role.service';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {
  
  roles:Role[];
  constructor(private authService:AuthService,
    private roleService:RoleService,
    private alertify:AlertifyService,
    private router:Router) { }

  ngOnInit(): void {
    this.getRoles();
  }
  isAdmin(){
    return this.authService.isAdmin();
  }
  getRoles(){
    return this.roleService.getRoles().subscribe(roles=>{
      this.roles=roles;
    })
  }
  deleteRole(id:number){
    return this.roleService.deleteRole(id).subscribe(()=>{
      this.alertify.success("Rol silindi");
      this.router.navigateByUrl('/home', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/roles']);
    });
    }, error=>{
      this.alertify.error(error);
    })
  }

}
