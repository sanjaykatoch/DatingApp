import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { error } from 'selenium-webdriver';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.css']
})
export class ServerErrorComponent implements OnInit {
error:any;
  constructor(private router:Router) {
    debugger;
    const navigation=this.router.getCurrentNavigation(); // for getting navigationerror By routing
    this.error=navigation?.extras?.state?.error;
   }

  ngOnInit(): void {
  }

}
