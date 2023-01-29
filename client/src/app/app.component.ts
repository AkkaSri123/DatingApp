import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { user } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Dating App1';
  users:any;
  constructor(private http: HttpClient, private AccountService : AccountService){}
  ngOnInit(): void {
    this.setCurrentUser();
  }

 

  setCurrentUser(){
    const userstring = localStorage.getItem('user');
    if(!userstring) return ;
    const user : user = JSON.parse(userstring);
    this.AccountService.setCurrentUser(user);
  }
}
