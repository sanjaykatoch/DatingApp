import { Component, NgModule, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { BsDropdownConfig } from 'ngx-bootstrap/dropdown';
import { CommonModule } from '@angular/common';
import { Observable, observable } from 'rxjs';
import { User } from '../Model/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr/';
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
  providers:[BsDropdownConfig]
})

export class NavComponent implements OnInit {
  model : any={};
  constructor(public account:AccountService,
    public router:Router,
    public toastr:ToastrService) { }
  ngOnInit(): void {
  
  }
  login() {
    this.account.login(this.model).subscribe(response => {
      console.log(response);
      this.router.navigateByUrl('/members');
    }, error => {
      this.toastr.error(error.error);
      this.router.navigateByUrl('/');
    })
  }
 
  logout() {
    this.account.logout();
    console.log('User is logged Out');
  }
}
