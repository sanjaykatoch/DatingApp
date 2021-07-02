import { Component, OnInit } from '@angular/core';
import { Member } from '../Model/member';
import { Pagination } from '../Model/pagination';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  members: Partial<Member[]>;
  predicates = "liked";
  pageNumber = 1;
  pageSize = 4;
  pagination: Pagination;
  constructor(private memberService: MembersService) { }

  ngOnInit(): void {
    this.loadLikes();
  }
  loadLikes() {
    this.memberService.getLikes(this.predicates, this.pageNumber, this.pageSize).subscribe(response => {
      this.members = response.result;
      this.pagination = response.pagination;
    })
  }
  pageChanged(event:any){
this.pageNumber=event.page;
this.loadLikes();
}
}
