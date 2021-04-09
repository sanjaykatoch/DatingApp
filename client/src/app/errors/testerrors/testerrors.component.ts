import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-testerrors',
  templateUrl: './testerrors.component.html',
  styleUrls: ['./testerrors.component.css']
})
export class TesterrorsComponent implements OnInit {
baseUrl='https://localhost:5001/api/';
ValidationErrors:string[]=[];
  constructor(private http:HttpClient) { }

  ngOnInit(): void {
  }
  get404Error() {
    this.http.get(this.baseUrl + 'buggy/not-found').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    })
  }
  get400Error() {
    debugger;
    this.http.get(this.baseUrl + 'buggy/bad-request').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    })
  }
  get500Error() {
    this.http.get(this.baseUrl + 'buggy/server-error').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    })
  }
  get401Error() {
    this.http.get(this.baseUrl + 'buggy/auth').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    })
  }
  get400ValidationError() {
    this.http.post(this.baseUrl + 'account/register',{}).subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
      this.ValidationErrors=error;
    })
  }
}
