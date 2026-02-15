import {Injectable, signal} from '@angular/core';
import {IUser} from './IUser';

@Injectable({
  providedIn: 'root',
})
export class IdentityService {
  private user = signal<IUser | null>(null)

  isSignedIn(){
    return !!this.user();
  }

  getUser(){
    return this.user;
  }

  setUser(user:IUser){
    this.user.set(user);
  }
}
