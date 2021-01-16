import { Component, NgModule, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { BsDropdownConfig } from 'ngx-bootstrap/dropdown';
import { CommonModule } from '@angular/common';
import { Observable, observable } from 'rxjs';
import { User } from '../Model/user';
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
  providers:[BsDropdownConfig]
})

export class NavComponent implements OnInit {
  model : any={};
  constructor(public account:AccountService) { }
  ngOnInit(): void {
  
  }
  login() {
    this.account.login(this.model).subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    })
  }
 
  logout() {
    this.account.logout();
    console.log('User is logged Out');
  }
}
