import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/Model/member';
import { User } from 'src/app/Model/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild("editForm") editForm:NgForm;
member:Member;
user:User;
//This hostListener add for prevent page refresh without save changes and move to another pages
@HostListener('window:beforeunload',['$event']) unloadNotification($event:any){
  if(this.editForm.dirty){
    $event.returnValue=true;
  }
}

  constructor(private accountService:AccountService,
    private memberService:MembersService,private toast:ToastrService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=>this.user=user);
  }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember(){
    this.memberService.getMember(this.user.userName).subscribe(
      member=>this.member=member
    );
  }
  updateMember(){
    this.memberService.updateMembers(this.member).subscribe(()=>{
      this.toast.success("profile is updated Suucessfully");
      this.editForm.reset(this.member);
    })
  
  }
}
