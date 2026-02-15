import {ChangeDetectionStrategy, Component, inject} from '@angular/core'
import {AuthLayoutService} from '../../../services/auth/auth-layout-service'

@Component({
  selector: 'email',
  imports: [],
  providers: [AuthLayoutService],
  templateUrl: './email.html',
  styleUrl: './email.scss',
  changeDetection: ChangeDetectionStrategy.Default,
})
export class Email {
  constructor(private authLayoutService: AuthLayoutService) {
  }

  ngOnInit() {
    this.authLayoutService.setFormTitle("I was here!")
  }
}
