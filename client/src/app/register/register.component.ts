import { Component, OnInit,Input, Output, EventEmitter } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
model:any={};
// @Input() userFromHomeComponnet:any;
@Output() cancelRegister=new EventEmitter();
  constructor(private account:AccountService) { }

  ngOnInit(): void {
  }
  register(model){
this.account.register(this.model).subscribe(response=>{
  console.log(response);
  this.cancel();
},error=>
{
  console.log(error);
})
}
cancel(){
  this.cancelRegister.emit(false);
  console.log('cancel Inside That');
}
}
