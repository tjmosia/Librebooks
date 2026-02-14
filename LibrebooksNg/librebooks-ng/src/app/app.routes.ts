import { Routes } from '@angular/router';
import {RootLayout} from './layouts/root-layout/root-layout';
import {AuthLayout} from './layouts/auth-layout/auth-layout';
import {Home} from './pages/app/home/home';
import {MainLayout} from './layouts/main-layout/main-layout';
import {authGuard} from './guards/auth-guard';

export const routes: Routes = [
  {
    path: '',
    component: RootLayout,
    children: [
      {
        path: '/',
        redirectTo: '/app/',
      },
      {
        path: "auth",
        component: AuthLayout,
        children: [
          {

          }
        ]
      },
      {
        path: "app",
        component: MainLayout,
        canActivate: [authGuard],
        children: [
          {
            path: "",
            component: Home
          }
        ]
      }
    ]
  }
];
