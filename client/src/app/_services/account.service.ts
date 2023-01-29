import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UrlSerializer } from '@angular/router';
import { BehaviorSubject, map } from 'rxjs';
import { user } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseURL ='https://localhost:5001/api/'

  private CurrentUserSource = new BehaviorSubject<user | null>(null);
  CurrentUser$ = this.CurrentUserSource.asObservable();

  constructor(private http:HttpClient) { }
  login(model:any){
   return this.http.post<user>(this.baseURL + 'Account/login',model).pipe(
    map((Response : user) => {
        const user = Response;
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
          this.CurrentUserSource.next(user); //we are setting up the value of currentUserSource as user
        }
    } 
    )
   );
  }
  register(model : any)
  {
    return this.http.post<user>(this.baseURL + 'Account/register',model).pipe(
      map(user => {
          if(user){
            localStorage.setItem('user',JSON.stringify(user));
            this.CurrentUserSource.next(user);
          }
          return user;
      })
    )
  }
  
  setCurrentUser(user : user){
    this.CurrentUserSource.next(user);
  }

  logout()
  {
    localStorage.removeItem('user');
    this.CurrentUserSource.next(null);
  }

}
