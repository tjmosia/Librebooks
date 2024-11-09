import { RouteObject } from 'react-router-dom'
import EntryLayout from './layouts/entry.layout'
import { HomePage } from './pages/home.page'
import LoginPage from './pages/auth/login.page'
import AuthLayout from './pages/auth/auth.layout'
import UsernamePage from './pages/auth/username.page'
import RegisterPage from './pages/auth/register.page'

const routes: RouteObject[] = [
	{
		path: "/",
		Component: EntryLayout,
		children: [
			{
				path: '/',
				Component: HomePage
			}
		]
	},
	{
		path: "/auth",
		Component: AuthLayout,
		children: [
			{
				path: "",
				Component: UsernamePage
			},
			{
				path: "verify",
				Component: UsernamePage
			},
			{
				path: "reset-password",
				Component: UsernamePage
			},
			{
				path: "login",
				Component: LoginPage
			},
			{
				path: "register",
				Component: RegisterPage
			}
		]
	}
]

export default routes