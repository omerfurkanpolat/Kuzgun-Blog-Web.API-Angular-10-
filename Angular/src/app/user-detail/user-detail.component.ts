import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import { Role } from '../_models/role';
import { User } from '../_models/user';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { RoleService } from '../_services/role.service';
import { UserManagerService } from '../_services/user-manager.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  isDelete:boolean;
  user: User;
  roles:Role[];
  model:any={};
  constructor(private authService: AuthService, 
    private alertifyService: AlertifyService, 
    private userManagerService: UserManagerService,
    private route:ActivatedRoute,
    private router:Router, 
    private roleService:RoleService  ) { }

  ngOnInit(): void {
    this.route.data.subscribe(data=>{
      this.user=data.user;
      this.getRoles();
    })
    

  }

  isAdmin() {
    return this.authService.isAdmin()
  }

  deleteUser(){   
    return this.userManagerService.deleteUser(this.route.snapshot.params['id']).subscribe(()=>{
            this.alertifyService.success("Hesap Kullanıma Kapatıldı");
            this.router.navigateByUrl('/home', { skipLocationChange: true }).then(() => {
              this.router.navigate(['usermanage/userdetail/'+this.route.snapshot.params['id']]);
          });
          }, error=>{
            this.alertifyService.error(error);
          }
          )
    };
    
    getRoles(){
      return this.roleService.getRoles().subscribe(roles=>{
        this.roles=roles;
      })
    }

    changeUserRole(id:number){
      this.roleService.changeUserRole(id, this.model).subscribe(()=>{
        this.alertifyService.success("Rol başarıyla değiştirildi");
        this.router.navigateByUrl('/home', { skipLocationChange: true }).then(() => {
          this.router.navigate(['usermanage/userdetail/'+this.route.snapshot.params['id']]);
      });
      },error=>{
        this.alertifyService.error(error);
      })
    }



}
