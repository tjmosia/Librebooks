import { RouteObject } from 'react-router-dom'
import EntryLayout from './layouts/entry.layout'
import { HomePage } from './pages/home.page'
import LoginPage from './pages/auth/login.page'
import AuthLayout from './pages/auth/auth.layout'
import UsernamePage from './pages/auth/username.page'
import RegisterPage from './pages/auth/register.page'
import VerifyEmailPage from './pages/auth/verify.page'
import ResetPasswordPage from './pages/auth/resetPasswod'

const routes: RouteObject[] = [
	{
		path: "/",
		element: <EntryLayout />,
		children: [
			{
				path: '/',
				element: <HomePage />
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