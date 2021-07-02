import { HttpClient, HttpHandler, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../Model/member';
import { PaginatedResult } from '../Model/pagination';
import { User } from '../Model/user';
import { UserParams } from '../Model/userParams';
import { AccountService } from './account.service';


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
memberCache=new Map();
user:User;
userParams:UserParams;

  constructor(private http: HttpClient,private accountService:AccountService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(response => {
      this.user = response;
      this.userParams = new UserParams(response);
    })
  }

  getUserParams() {
    return this.userParams;
  }
  setUserParams(params:UserParams) {
    this.userParams = params;
  }

  resetUserParams(){
    this.userParams=new UserParams(this.user);
    return this.userParams;
  }
  getMembers(userParmas:UserParams) {
    
    //only used for cahcing records prevent unnessary hits
    //to Database
    var response=this.memberCache.get(Object.values(userParmas).join('-'));
    if(response){
      return of(response);
    }
    let params = this.getPaginationHeaders(userParmas.pageNumber,userParmas.pageSize);

    params = params.append('maxAge', userParmas.maxAge.toString());
    params = params.append('minAge', userParmas.minAge.toString());
    params = params.append('gender', userParmas.gender.toString());
    params = params.append('orderBy', userParmas.orderBy.toString());
    // if (this.members.length > 0) return of(this.members);
    
    return this.getPaginatedResult<Member[]>(this.baseUrl+'users',params)
    .pipe(map(response=>{
      this.memberCache.set(Object.values(userParmas).join('-'),response);
     return response;
    }))
  }
  addLike(userName: string) {
    return this.http.post(this.baseUrl+'likes/'+userName,{});

    // return this.http.post(this.baseUrl + 'likes/', { userName: userName });
  }
  getLikes(predicates: string,pageNumber,pageSize) {
    let params=this.getPaginationHeaders(pageNumber,pageSize);
    params=params.append('predicate',predicates);//append the predicates
    return this.getPaginatedResult<Partial<Member[]>>(this.baseUrl+'likes',params);
  //  return this.http.get<Partial<Member[]>>(this.baseUrl + 'likes?predicate=' + predicates);

    // return this.http.get(this.baseUrl+'likes',{predicates});
  }
  getMember = (username: string)=> {
    // const member = this.members.find(x => x.userName === username);
    // if (member !== undefined) return of(member);
    const Member=[...this.memberCache.values()]
    .reduce((arr,elem)=>arr.concat(elem.result),[])
    .find((member:Member)=>member.userName==username);
    console.log(Member);
    if(Member){
      return of(Member);
    }

    return this.http.get<any>(this.baseUrl + 'users/' + username);
      // .pipe(
      //   map((result => {
      //     const index = this.members.indexOf(result);
      //     if(index!=-1){
      //       this.members[index] = result;
      //       return this.members[index];
      //     }else{
      //       return result;
      //     }
          
      //   }))
      // )

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
  private getPaginatedResult<T>(url,params){
    const paginatedResult:PaginatedResult<T>=new PaginatedResult<T>();
 
     return this.http.get<T>(url, { observe: 'response', params }).pipe(
       map(response => {
         paginatedResult.result = response.body;
         if (response.headers.get('Pagination') !== null) {
           paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
         }
         return paginatedResult;
       })
     )
   }
   private getPaginationHeaders(pageNumber: number, pageSize: number) {
     let params = new HttpParams();
     if (pageNumber != null && pageSize != null) {
       params = params.append('pageNumber', pageNumber.toString());
       params = params.append('pageSize', pageSize.toString());
     }
     return params;
   }
}
