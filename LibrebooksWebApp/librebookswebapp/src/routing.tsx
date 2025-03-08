import { Route, Routes } from 'react-router-dom'
import { HomePage } from './app/pages/HomePage'
import LoginPage from './app/pages/auth/LoginPage'
import AuthLayout from './app/pages/auth/AuthLayout'
import UsernamePage from './app/pages/auth/UsernamePage'
import RegisterPage from './app/pages/auth/RegisterPage'
import VerifyEmailPage from './app/pages/auth/VerifyEmailPage'
import ResetPasswordPage from './app/pages/account/ResetPasswordFragment'
import MainLayout from './app/layouts/MainLayout'
import AccountPage from './app/pages/account/AccountPage'
import RootLayout from './app/layouts/RootLayout'

export default function Routing(){
	return (
		<>
			<Routes>
				<Route path='*' element={<RootLayout />}>
					<Route path="/" element={<MainLayout />}>
						<Route path="" element={<HomePage />} />
						<Route path="auth*" element={<AuthLayout />}>
							<Route path="" element={<UsernamePage />} />
							<Route path="verify" element={<VerifyEmailPage />} />
							<Route path="reset-password" element={<ResetPasswordPage />} />
							<Route path="change-password" element={<ResetPasswordPage />} />
							<Route path="login" element={<LoginPage />} />
							<Route path="register" element={<RegisterPage />} />
						</Route>
						<Route path='account/*' element={<AccountPage />}  />
					</Route>
				</Route>
			</Routes>
		</>
	)
}
