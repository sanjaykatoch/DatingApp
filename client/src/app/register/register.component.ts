import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  maxDate:Date;
  validationErrors:string[]= [];
  // @Input() userFromHomeComponnet:any;
  @Output() cancelRegister = new EventEmitter();
  constructor(private account: AccountService,
    private toastr: ToastrService,private formBuilder: FormBuilder,private router:Router) { }

  ngOnInit(): void {
    this.InitializeForm();
    this.maxDate=new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }
  InitializeForm() {
    this.registerForm = this.formBuilder.group(
      {
       username: ["", [Validators.required]],
        gender: ["male", [Validators.required]],
        knownAs: ["", [Validators.required]],
        dateOfBirth: ['', [Validators.required]],
        city: ["", [Validators.required]],
        country: ["", [Validators.required]],
        password: ["", [Validators.required, Validators.minLength(4),Validators.maxLength(8)]],
      
        confirmPassword: ["", [Validators.required,this.matchValues('password')]]
      }
    );
    this.registerForm.controls.password.valueChanges.subscribe(()=>{
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
       
    })
    // this.registerForm = new FormGroup({
    //   username: new FormControl(),
    //   password: new FormControl(),
    //   confirmPassword: new FormControl()
    // });
    // this.registerForm = new FormGroup({
    //   username: new FormControl(null, Validators.required),
    //   password: new FormControl(null, [Validators.required, Validators.minLength(4), Validators.maxLength(8)]),
    //   confirmPassword: new FormControl(null, Validators.required)
    // });
  }
  matchValues(macthTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[macthTo].value?null: { isMatching: true }
    }

  }
  register() {
  //  console.log(this.registerForm.value);
    this.account.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('/members');
    }, error => {
this.validationErrors=error;
      //this.toastr.error(error.error);
     // console.log(error);
    })
  }
  cancel() {
    this.cancelRegister.emit(false);
    console.log('cancel Inside That');
  }
}
