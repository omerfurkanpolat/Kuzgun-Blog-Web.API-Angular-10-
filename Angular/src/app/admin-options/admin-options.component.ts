import { Component, OnInit } from '@angular/core';
import { Role } from '../_models/role';
import { RoleService } from '../_services/role.service';

@Component({
  selector: 'app-admin-options',
  templateUrl: './admin-options.component.html',
  styleUrls: ['./admin-options.component.css']
})
export class AdminOptionsComponent implements OnInit {
roles:Role[];
  constructor(private roleService:RoleService) { }

  ngOnInit(): void {
  }

  getRoles(){
    return this.roleService.getRoles().subscribe(roles=>{
      this.roles=roles;
    })
  }

}
