import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
registermode = false;
users : any;
  constructor(private http : HttpClient) { }

  ngOnInit(): void {
    this.getUsers();
  }
  registerToggle(){
    this.registermode= !this.registermode;
  }

  getUsers(){
    this.http.get('https://localhost:5001/api/user').subscribe({
      next : response => this.users = response,
      error : error => console.log(error),
      complete : () => console.log('Request completed')
    })
  }

  cancelRegisterMode(event : boolean)
  {
    this.registermode=event;
  }

}
