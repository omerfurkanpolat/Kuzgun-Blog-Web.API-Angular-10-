import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { Messagge } from '../_models/messagge';

@Injectable({
    providedIn:"root"
})
export class MessageService{

    baseUrl:string="https://localhost:44386/api/message";
    constructor(private http:HttpClient){}

    sendMessage(model:any){
      return   this.http.post(this.baseUrl+'/sendmessage', model);
    }
    answerMessage(message:Messagge){
      return   this.http.post(this.baseUrl+'/AnswerMessage', message);
    }

  getMessages():Observable<Messagge[]>{
    return this.http.get<Messagge[]>(this.baseUrl+'/getmessages')
  }

  getMessage(id:number):Observable<Messagge>{
    return this.http.get<Messagge>(this.baseUrl+'/getmessage/' +id)
  }



}