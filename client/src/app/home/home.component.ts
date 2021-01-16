import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from '../Model/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
//registerMode:boolean;
users:any;
registerMode=false;

  constructor(private http:HttpClient) { }

  ngOnInit(): void {
    this.getUser();
    //this.registerMode=false;
  }
  getUser() {
    this.http.get("https://localhost:5001/api/users").subscribe(Response => {
      this.users = Response;
    }, error => {
      console.log(error);
    })
  }
  getUserAgain() {
    this.http.get("https:localhost:50001/users").subscribe(Response => {
      this.users = Response;
    }, error => {
      console.log(error);
    })
  }
registerToggle(){
  this.registerMode  =!this.registerMode;
}
cancelRegisterMode(event:boolean){
  this.registerMode=event;
}
}
