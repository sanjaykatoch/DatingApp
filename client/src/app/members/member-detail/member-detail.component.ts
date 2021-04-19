import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/Model/member';
import { MembersService } from 'src/app/_services/members.service';
import { CommonModule } from '@angular/common';  
import { BrowserModule } from '@angular/platform-browser';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
member:Member;
galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  
  constructor(private memberService:MembersService,private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMember();
    
    this.galleryOptions=[
      {
        width: '500px',
        height: '500px',
        imagePercent:100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview:false
      }
    ];
 
  }

  getImages(): NgxGalleryImage[] {
    const ImageUrl = [];
    for (const photo of this.member.photos) {
      ImageUrl.push({
        small:photo?.url,
        medium:photo?.url,
        big:photo?.url
      });
    }
    return ImageUrl;
  }
  loadMember(){
    this.memberService.getMember(this.route.snapshot.paramMap.get('username'))
    .subscribe(member=>{
      this.member=member;
      this.galleryImages=this.getImages();
    })
  }
}
