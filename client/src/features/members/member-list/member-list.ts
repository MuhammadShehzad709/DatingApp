import { Component, inject } from '@angular/core';
import { MemberService } from '../../../core/services/member-service';
import { map, Observable } from 'rxjs';
import { Member } from '../../../types/member';
import { AsyncPipe } from '@angular/common';
import { ApiResponse } from '../../../types/ApiResponse';
import { MemberCard } from "../member-card/member-card";

@Component({
  selector: 'app-member-list',
  imports: [AsyncPipe, MemberCard],
  templateUrl: './member-list.html',
  styleUrl: './member-list.css'
})
export class MemberList {
  private memberService = inject(MemberService);
  protected members$: Observable<Member[] | null>;

  constructor() {
    this.members$ = this.memberService.getMembers().pipe(
      map(response=>response.data)
    );
  }
}
