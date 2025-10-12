import { Component, inject, OnInit, signal, Signal } from '@angular/core';
import { MemberService } from '../../../core/services/member-service';
import { map, Observable, tap } from 'rxjs';
import { Member, Photo } from '../../../types/member';
import { ActivatedRoute } from '@angular/router';
import { EventInfoWrapper } from '@angular/core/primitives/event-dispatch';
import { AsyncPipe } from '@angular/common';
import { ImageUpload } from "../../../shared/image-upload/image-upload";
import { errorContext } from 'rxjs/internal/util/errorContext';
import { AccountService } from '../../../core/services/account-service';
import { User } from '../../../types/user';
import { StarButton } from "../../../shared/star-button/star-button";
import { DeleteButton } from "../../../shared/delete-button/delete-button";

@Component({
  selector: 'app-member-photos',
  imports: [ImageUpload, StarButton, DeleteButton],
  templateUrl: './member-photos.html',
  styleUrl: './member-photos.css'
})
export class MemberPhotos implements OnInit {
  protected accountService = inject(AccountService);
  protected memeberService = inject(MemberService);
  private route = inject(ActivatedRoute)
  protected photos = signal<Photo[]>([]);
  protected loading = signal(false);
  ngOnInit(): void {
    const memberId = this.route.parent?.snapshot.paramMap.get('id');
    if (memberId) {
      this.memeberService.getMemberPhoto(memberId).subscribe({
        next: response => {
          if (response.data) {
            this.photos.set(response.data);
          }
        }
      })
    }
  }
  onUploadImage(file: File) {
    this.loading.set(true);
    this.memeberService.uploadPhoto(file).subscribe({
      next: response => {
        if (response.data && response.isSuccess) {
          this.memeberService.editMode.set(false);
          this.loading.set(false);
          this.photos.update(photos => [...photos, response.data!])
          if(!this.memeberService.member()?.imageUrl){
            this.setMainLocalPhoto(response.data);
          }
        } else {
          console.log('Error Uplading image:', response.error)
        }
      },
      error: error => {
        console.log('Error Uplading image:', error)
        this.loading.set(false);
      }
    })
  }
  setMainPhoto(photo: Photo) {
    this.memeberService.setMainPhoto(photo).subscribe({
      next: response => {
        if (response.isSuccess) {
          this.setMainLocalPhoto(photo);
        }
      }
    })
  }
  deletePhoto(photoId: number) {
    console.log('helo')
    this.memeberService.deletePhoto(photoId).subscribe({
      next: response => {
        if (response.isSuccess) {
          this.photos.update(photos => photos.filter(x => x.id !== photoId))
          console.log('hleoo')
        }
      }
    })
  }
  private setMainLocalPhoto(photo:Photo) {
    const currentUser = this.accountService.currentUser();
    if (currentUser) currentUser.imageUrl = photo.url;
    this.accountService.setCurrentUser(currentUser as User);
    this.memeberService.member.update(member => ({
      ...member,
      imageUrl: photo.url
    }) as Member)
  }
  // getMock(){
  //   return Array.from({length:20},(_,i)=>({
  //     url:'/mansoor2.jpg'
  //   }))
  // }
}
