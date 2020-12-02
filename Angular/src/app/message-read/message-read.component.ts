
import { Component, OnInit } from '@angular/core';
import { Messagge } from '../_models/messagge';
import { AuthService } from '../_services/auth.service';
import { MessageService } from '../_services/message.service';

@Component({
  selector: 'app-message-read',
  templateUrl: './message-read.component.html',
  styleUrls: ['./message-read.component.css']
})
export class MessageReadComponent implements OnInit {

  messages:Messagge[];


  constructor(private authService:AuthService,
    private messageService:MessageService) { }

  ngOnInit(): void {
    this.getMessages();
  }

  isAdmin(){
    return this.authService.isAdmin();
  }

  getMessages(){
    this.messageService.getMessages().subscribe(messages=>{
      this.messages=messages;
      
    })
  }
}
