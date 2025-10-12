import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { EditableMember, Member, Photo } from '../../types/member';
import { ApiResponse } from '../../types/ApiResponse';
import { Conditional } from '@angular/compiler';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MemberService {
  private http = inject(HttpClient);
  editMode=signal(false);
  baseUrl = environment.apiUrl;
  member=signal<Member | null>(null);
  getMembers() {
    return this.http.get<ApiResponse<Member[]>>(this.baseUrl + 'members');
  }
  getMember(id:string){
    return this.http.get<ApiResponse<Member>>(this.baseUrl+'members/'+id).pipe(
      tap(member=>{
        this.member.set(member.data)
      })
    )
  }
  getMemberPhoto(id:string){
    return this.http.get<ApiResponse<Photo[]>>(this.baseUrl+'members/'+id+'/photos')
  }
  updateMember(member:EditableMember){
    return this.http.put<ApiResponse<number>>(this.baseUrl+'members',member)
  }
  uploadPhoto(file:File){
    const formData=new FormData();
    formData.append('file',file);
    return this.http.post<ApiResponse<Photo>>(this.baseUrl+'members/add-photo',formData)
  }
  setMainPhoto(photo:Photo){
    return this.http.put<ApiResponse<number>>(this.baseUrl+'members/set-main-photo/'+photo.id,{});
  }
  deletePhoto(photoId:number){
    return this.http.delete<ApiResponse<number>>(this.baseUrl+'members/delete-photo/'+photoId)
  }
}
