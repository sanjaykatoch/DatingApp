import { Input } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { Member } from 'src/app/Model/member';
import { User } from 'src/app/Model/user';
import { AccountService } from 'src/app/_services/account.service';
import { environment } from 'src/environments/environment';
import { take } from 'rxjs/operators';
import { MembersService } from 'src/app/_services/members.service';
import { Photo } from 'src/app/Model/photo';


@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() member: Member;
  uploader: FileUploader;
  hasBaseDropzoneOver = false;
  baseUrl = environment.apiUrl;
  user: User;
  constructor(private acccountService: AccountService,private memberService: MembersService) {
    this.acccountService.currentUser$.pipe(take(1)).subscribe(user =>
      this.user = user
    )
  }

  ngOnInit(): void {
    this.IntializeUploader();
  }
  fileOverBase(e: any) {
    debugger;
    this.hasBaseDropzoneOver = e;

  }
  setMainPhoto(photo: Photo) {
    this.memberService.SetMainPhoto(photo.id).subscribe(() => {
      this.user.photoUrl = photo.url;
      this.acccountService.setCuurentUser(this.user);
      this.member.photoUrl = photo.url;
      this.member.photos.forEach(p => {
        //if(p.isMain) p.isMain=false;
        if (p.id == photo.id) p.isMain = true;
        else p.isMain = false;
      })
    });
  }
  deletePhoto(photoId: number){
    this.memberService.deletePhoto(photoId).subscribe(()=>{
      this.member.photos=this.member.photos.filter(x=>x.id!=photoId);
    })
  }
  IntializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/add-photo',
      authToken: 'Bearer ' + this.user.token,
      isHTML5: true,
      allowedFileType: ['Image'],
      removeAfterUpload: true,
      autoUpload: false,

      // authTokenHeader: 'authorization',
      maxFileSize: 10 * 1024 * 1024
    });
    

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }
    this.uploader.onCompleteItem=(item, response, status, headers)=>{
      if (response) {
        const photo = JSON.parse(response);
        this.member.photos.push(photo);
      }
    }
    // this.uploader.onSuccessItem = (item, response, status, headers) => {
    //   debugger;
    //   if (response) {
    //     const photo = JSON.parse(response);
    //     this.member.photos.push(photo);
    //   }
    // }
  }

}
