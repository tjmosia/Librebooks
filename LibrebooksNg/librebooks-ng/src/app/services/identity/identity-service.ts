import { Injectable } from '@angular/core';
import {IUser} from './IUser';

@Injectable({
  providedIn: 'root',
})
export class IdentityService {
  private user?: IUser;

  isSignedIn(){
    return !!this.user;
  }
  getUser(){
    return this.user; 
  }
}
