import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { UserManagerService } from '../_services/user-manager.service';

@Component({
  selector: 'app-user-manage',
  templateUrl: './user-manage.component.html',
  styleUrls: ['./user-manage.component.css']
})
export class UserManageComponent implements OnInit {

  constructor(private authService: AuthService,
    private userManageService: UserManagerService,
    private alertifyService: AlertifyService) { }
  users: User[];
  ngOnInit(): void {
    this.getUsers();
  }

  isAdmin() {
    return this.authService.isAdmin();
  }

  getUsers() {
    this.userManageService.getUsers().subscribe(users => {
      this.users = users;

    }, error => {
      this.alertifyService.error(error);
    })
  }

}
