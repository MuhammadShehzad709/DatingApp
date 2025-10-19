import { Component, inject, OnInit, signal, ViewChild } from '@angular/core';
import { MemberService } from '../../../core/services/member-service';
import { Member, MemberParams } from '../../../types/member';
import { MemberCard } from "../member-card/member-card";
import { PaginatedResult } from '../../../types/Pagination';
import { Paginator } from "../../../shared/paginator/paginator";
import { FilterModel } from '../filter-model/filter-model';
import { filter } from 'rxjs';

@Component({
  selector: 'app-member-list',
  imports: [MemberCard, Paginator, FilterModel],
  templateUrl: './member-list.html',
  styleUrl: './member-list.css'
})
export class MemberList implements OnInit {
  @ViewChild('filterModal') modal!:FilterModel;
  private memberService = inject(MemberService);
  protected paginatedMembers=signal<PaginatedResult<Member> | null>(null);
  protected memberParams=new MemberParams()
  private updatedMember=new MemberParams();
  constructor() {
    const filters=localStorage.getItem('filter');
    if(filters){
      this.memberParams=JSON.parse(filters);
    }
    
  }
  ngOnInit(): void {
     this.loadMembers()
  }
  loadMembers() {
  this.memberService.getMembers(this.memberParams).subscribe({
    next:response=>{
      this.paginatedMembers.set(response.data)
    }
  })
  }
  onPageChange(event:{pageNumber:number,pageSize:number}){
    this.memberParams.pageSize=event.pageSize;
    this.memberParams.pageNumber=event.pageNumber;
    this.loadMembers();
  }
  openModal(){
    this.modal.open();
  }
  onClose(){
    console.log('Model Closed')
  }
  onFilterChange(data:MemberParams){
    this.memberParams={...data};
    this.updatedMember={...data};
    this.loadMembers()
  }
  resetFilters(){
    this.memberParams=new MemberParams();
    this.updatedMember=new MemberParams();
    this.loadMembers();
  }
  get displayMessage():string{
    const defaultParams=new MemberParams();
    const filters:string[]=[];
    if(this.updatedMember.gender){
      filters.push(this.updatedMember.gender+'s')
    }else{
      filters.push('Males,Females');
    }
    if(this.updatedMember.minAge!==defaultParams.minAge || this.updatedMember.maxAge!=defaultParams.maxAge){
      filters.push(` ages ${this.updatedMember.minAge}-${this.updatedMember.maxAge}`)
    }
    filters.push(this.updatedMember.orderBy==='lastActive' ? 'Recently active': 'Newest members');
    return filters.length>0 ? `selected: ${filters.join('  | ')}`:`All members`
  }
}