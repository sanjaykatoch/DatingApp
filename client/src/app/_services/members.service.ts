import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { Observable } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { environment } from 'src/environments/environment';
import { Member } from '../Model/member';


// const httpOptions={
//   headers:new HttpHeaders({
//     Authorization:'Bearer '+JSON.parse(localStorage.getItem('users'))
//   })
// }

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = []; 

  constructor(private http: HttpClient) { }

  getMembers(): Observable<Member[]> {
    if (this.members.length > 0) return of(this.members);
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map(members => {
        this.members = members;
        return members;
      })
    )
  }
  getMember = (username: string)=> {
    const member = this.members.find(x => x.userName === username);
    if (member !== undefined) return of(member);
    return this.http.get<any>(this.baseUrl + 'users/' + username)
      .pipe(
        map((result) => {
          const index = this.members.indexOf(result);
          if(index!=-1){
            this.members[index] = result;
            return this.members[index];
          }else{
            return result;
          }
          
        })
      )

  }
  updateMembers(member: Member) {
    return this.http.put(this.baseUrl + 'users', member);

  }
  SetMainPhoto(photoId:number){
    return this.http.put(this.baseUrl+'users/set-main-photo/' + photoId,{});

  }
  deletePhoto(photoId:number){
    return this.http.delete(this.baseUrl+'users/delete-photo/' + photoId);

  }
}
