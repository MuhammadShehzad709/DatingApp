import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { EditableMember, Member, MemberParams, Photo } from '../../types/member';
import { ApiResponse } from '../../types/ApiResponse';
import { Conditional } from '@angular/compiler';
import { tap } from 'rxjs';
import { PaginatedResult } from '../../types/Pagination';

@Injectable({
  providedIn: 'root'
})


export class MemberService {
  gender?: string;
  minAge = 18;
  maxAge = 100;
  pageNumber = 1;
  pageSize = 10;
  private http = inject(HttpClient);
  editMode = signal(false);
  baseUrl = environment.apiUrl;
  member = signal<Member | null>(null);
  getMembers(memberParmas: MemberParams) {
    let params = new HttpParams();
    params = params.append('pageNumber', memberParmas.pageNumber);
    params = params.append('pageSize', memberParmas.pageSize)
    params = params.append('minAge', memberParmas.minAge)
    params = params.append('maxAge', memberParmas.maxAge)
    params = params.append('orderBy', memberParmas.orderBy)
    if (memberParmas.gender) params = params.append('gender', memberParmas.gender)

    return this.http.get<ApiResponse<PaginatedResult<Member>>>(this.baseUrl + 'members', { params }).pipe(
      tap(()=>{
        localStorage.setItem('filter',JSON.stringify(memberParmas));
      })
    );
  }
  getMember(id: string) {
    return this.http.get<ApiResponse<Member>>(this.baseUrl + 'members/' + id).pipe(
      tap(member => {
        this.member.set(member.data)
      })
    )
  }
  getMemberPhoto(id: string) {
    return this.http.get<ApiResponse<Photo[]>>(this.baseUrl + 'members/' + id + '/photos')
  }
  updateMember(member: EditableMember) {
    return this.http.put<ApiResponse<number>>(this.baseUrl + 'members', member)
  }
  uploadPhoto(file: File) {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<ApiResponse<Photo>>(this.baseUrl + 'members/add-photo', formData)
  }
  setMainPhoto(photo: Photo) {
    return this.http.put<ApiResponse<number>>(this.baseUrl + 'members/set-main-photo/' + photo.id, {});
  }
  deletePhoto(photoId: number) {
    return this.http.delete<ApiResponse<number>>(this.baseUrl + 'members/delete-photo/' + photoId)
  }
}
