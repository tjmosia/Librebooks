import { RouteObject } from 'react-router-dom'
import { HomePage } from './pages/HomePage'
import LoginPage from './pages/auth/LoginPage'
import AuthLayout from './pages/auth/AuthLayout'
import UsernamePage from './pages/auth/UsernamePage'
import RegisterPage from './pages/auth/RegisterPage'
import VerifyEmailPage from './pages/auth/VerifyEmailPage'
import ResetPasswordPage from './pages/account/ResetPasswordFragment'
import { CreateCompanyPage } from './pages/companies/CreateCompanyPage'
import MainLayout from './layouts/MainLayout'
import AccountPage from './pages/account/AccountPage'
import { InventoryRoutes } from './pages/inventory'
import CustomersRoutes from './pages/customers/CustomersRoutes'

const routes: RouteObject[] = [
	{
		path: "/",
		element: <MainLayout />,
		children: [
			{
				path: '',
				element: <HomePage />
			},
			{
				path: 'companies/create',
				element: <CreateCompanyPage />
			},
			{
				path: "account/*",
				element: <AccountPage />
			},
			...InventoryRoutes,
			...CustomersRoutes
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
				path: "change-password",
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