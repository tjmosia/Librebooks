import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthLayoutService {
  formTitle?: string;
  formMessage?: string;

  constructor(private authLayoutService: AuthLayoutService) {
  }

  setFormTitle(title: string){
    this.formTitle = title;
  }

  setFormMessage(message: string){
    this.formMessage = message;
  }
}
