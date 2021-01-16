import { JsonPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './Model/user';
import { AccountService } from './_services/account.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'The Dating App';
 // users:any;

 constructor(private http:HttpClient,private accountService:AccountService){

  }
  ngOnInit() {
 // this.getUser();
  this.setCurrentUser();
}
setCurrentUser()
{
const user: User=JSON.parse(localStorage.getItem('user'));
this.accountService.setCuurentUser(user);
}
  // getUser() {
  //   this.http.get("https://localhost:5001/api/users").subscribe(Response => {
  //     this.users = Response;
  //   }, error => {
  //     console.log(error);
  //   })
  // }
}