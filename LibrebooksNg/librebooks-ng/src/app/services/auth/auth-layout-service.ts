import {Injectable, signal} from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthLayoutService {
  private formTitle = signal<string>("");
  private formMessage = signal<string>("");

  setFormTitle(title: string){
    this.formTitle.set(title);
  }

  setFormMessage(message: string){
    this.formMessage.set(message);
  }

  getFormMessage(){
    return this.formMessage.asReadonly()();
  }

  getFormTitle(){
    return this.formTitle.asReadonly()();
  }
}
