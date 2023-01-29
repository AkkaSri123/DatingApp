import { HttpClient } from '@angular/common/http';
import { outputAst } from '@angular/compiler';
import { Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
 @Output() CancelRegister = new EventEmitter();
  model : any = {};
  constructor(private accountService : AccountService) { }

  ngOnInit(): void {
  }

  register(){
   this.accountService.register(this.model).subscribe({
    next : Response =>{
      console.log(Response);
      this.cancel();
    },
    error : error => console.log(error)
   })
  }

  cancel(){
    this.CancelRegister.emit(false);
  }

}