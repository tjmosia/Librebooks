import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {AuthLayoutService} from '../../services/auth/auth-layout-service';

@Component({
  selector: 'app-auth-layout',
  imports: [
    RouterOutlet
  ],
  templateUrl: './auth-layout.html',
  styleUrl: './auth-layout.scss',
  changeDetection: ChangeDetectionStrategy.Default,
})
export class AuthLayout {
  authLayoutService = inject(AuthLayoutService);

  ngOnInit() {
  }
}
