import { CanDeactivateFn } from '@angular/router';
import { MemberProfile } from '../../features/members/member-profile/member-profile';

export const preventUnsavedChangesGuard: CanDeactivateFn<MemberProfile> = (component, currentRoute, currentState, nextState) => {
  if(component.editForm?.dirty){
    return confirm("Are u sure u want to continue? All unsaved changes will be lost")
  }
  return true;
};
