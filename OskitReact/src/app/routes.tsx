import { RouteObject } from 'react-router-dom'
import { HomePage } from './pages/HomePage'
import LoginPage from './pages/auth/LoginPage'
import AuthLayout from './pages/auth/AuthLayout'
import UsernamePage from './pages/auth/UsernamePage'
import RegisterPage from './pages/auth/RegisterPage'
import VerifyEmailPage from './pages/auth/VerifyEmailPage'
import ResetPasswordPage from './pages/auth/ResetPasswordPage'
import { CreateCompanyPage } from './pages/companies/wizard/CreateCompanyPage'
import MainLayout from './layouts/MainLayout'
import ProfilePage from './pages/account/ProfilePage'
import AccountLayout from './pages/account/AccountLayout'

const routes: RouteObject[] = [
	{
		path: "/",
		element: <MainLayout />,
		children: [
			{
				path: '/',
				element: <HomePage />
			},
			{
				path: '/companies/create',
				element: <CreateCompanyPage />
			},
			{
				path: "/account",
				element: <AccountLayout />,
				children: [
					{
						path: '',
						element: <ProfilePage />
					}
				]
			}
		]
	},
	{
		path: "/auth",
		element: <AuthLayout />,
		children: [
			{
				path: "",
				element: <UsernamePage />
			},
			{
				path: "verify",
				element: <VerifyEmailPage />
			},
			{
				path: "reset-password",
				element: <ResetPasswordPage />
			},
			{
				path: "login",
				element: <LoginPage />
			},
			{
				path: "register",
				element: <RegisterPage />
			}
		]
	}
]

export default routes