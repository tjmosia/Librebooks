import {CanActivateFn, Router} from '@angular/router';
import {IdentityService} from '../../services/identity/identity-service';
import {inject} from '@angular/core';

export const authGuard: CanActivateFn = async (route, state) => {
  const identityService = inject(IdentityService)
  const router = inject(Router);

  if(identityService.isSignedIn())
    return true;
  else{
    await router.navigate(['/auth']);
    return false;
  }
};
