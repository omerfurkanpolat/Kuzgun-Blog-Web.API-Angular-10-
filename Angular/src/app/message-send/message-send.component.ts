import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertifyjs';
import { MessageService } from '../_services/message.service';

@Component({
  selector: 'app-message-send',
  templateUrl: './message-send.component.html',
  styleUrls: ['./message-send.component.css']
})
export class MessageSendComponent implements OnInit {
  
  model:any={};
  constructor(private messageService:MessageService,
    private alertifyService:AlertifyService,private router:Router) { }

  ngOnInit(): void {
  }

  sendMessage(){
    this.messageService.sendMessage(this.model).subscribe(()=>{
      this.alertifyService.success("Mesajınız İletildi");
      this.router.navigateByUrl('/home', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/sendmessage']);
    });
    },error=>{
      this.alertifyService.error(error);
    } );
  }

}
