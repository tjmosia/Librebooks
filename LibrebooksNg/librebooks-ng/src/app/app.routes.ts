import { Routes } from '@angular/router';
import {RootLayout} from './layouts/root-layout/root-layout';
import {AuthLayout} from './layouts/auth-layout/auth-layout';
import {Home} from './pages/app/home/home';
import {MainLayout} from './layouts/main-layout/main-layout';
import {authGuard} from './guards/auth-guard/auth-guard';
import {Login} from './pages/auth/login/login';
import {Email} from './pages/auth/email/email';
import {Register} from './pages/auth/register/register';

export const routes: Routes = [
  {
    path: '',
    component: RootLayout,
    children: [
      {
        path: '',
        redirectTo: '/app',
        pathMatch: 'full',
      },
      {
        path: "auth",
        component: AuthLayout,
        children: [
          {
            path: "",
            component:  Email,
            pathMatch: "full",
          },
          {
            path: "login",
            component:  Login
          },
          {
            path: "register",
            component:  Register
          }
        ]
      },
      {
        path: "app",
        canActivate: [authGuard],
        component: MainLayout,
        pathMatch: 'full',
        children: [
          {
            path: "",
            component: Home,
            pathMatch: 'full',
          }
        ]
      }
    ]
  }
];
