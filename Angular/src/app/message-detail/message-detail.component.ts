import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Messagge } from '../_models/messagge';
import { AlertifyService } from '../_services/alertifyjs';
import { AuthService } from '../_services/auth.service';
import { MessageService } from '../_services/message.service';

@Component({
  selector: 'app-message-detail',
  templateUrl: './message-detail.component.html',
  styleUrls: ['./message-detail.component.css']
})
export class MessageDetailComponent implements OnInit {

  messagge:Messagge;
  model:any={};

  constructor(private authService:AuthService,
    private messageService:MessageService,
    private route:ActivatedRoute,
    private alertifyService:AlertifyService) { }

  ngOnInit(): void {
    this.route.data.subscribe(data=>{
      this.messagge=data.messagge;
    })
  }

  isAdmin(){
    return this.authService.isAdmin();
  }

  sendMessage(){
    this.messageService.answerMessage(this.messagge).subscribe(()=>{
      this.alertifyService.success("Yanıt Gönderildi");
    }, error=>{
      this.alertifyService.error(error);
    })
  }

}
