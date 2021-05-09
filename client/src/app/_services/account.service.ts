import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import{map} from 'rxjs/operators'
import { User } from '../Model/user';
import{environment} from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
// baseUrl="https://localhost:5001/api/";
baseUrl=environment.apiUrl; //this is URl is get from environment

  constructor(private http:HttpClient) { }

  private currentUserSource=new ReplaySubject<User>(1);
  currentUser$=this.currentUserSource.asObservable();

  login(model: any) {
    debugger;
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          debugger;
          this.setCuurentUser(user);
          // localStorage.setItem('user', JSON.stringify(user));
          // this.currentUserSource.next(user);
        }
      })
    )
  }
  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCuurentUser(user);
        //  this.currentUserSource.next(user);
        }
return "User is created";
      })
    )
  }
  setCuurentUser(user:User)
  {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }
  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
