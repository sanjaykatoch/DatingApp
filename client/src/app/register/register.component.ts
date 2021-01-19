import { Component, OnInit,Input, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
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
  constructor(private account:AccountService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
  }
  register(model){
this.account.register(this.model).subscribe(response=>{
  console.log(response);
  this.cancel();
},error=>
{
  this.toastr.error(error.error);
  console.log(error);
})
}
cancel(){
  this.cancelRegister.emit(false);
  console.log('cancel Inside That');
}
}
