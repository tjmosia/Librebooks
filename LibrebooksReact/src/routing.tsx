import { Route, Routes } from "react-router";
import AuthLayout from "./layouts/AuthLayout";
import LoginPage from "./app/identity/auth/login/Login";
import RootLayout from "./layouts/RootLayout";
import HomePage from "./app/Home";
import SessionLayout from "./layouts/SessionLayout";
import AdminLayout from "./layouts/AdminLayout";
import RegisterPage from "./app/identity/auth/register/Register";
import ForgotPasswordPage from "./app/identity/auth/forgot-password/ForgotPassword";
import ResetPasswordPage from "./app/identity/auth/reset-password/ResetPassword";
import VerifyEmailPage from "./app/identity/auth/verify-email/VerifyEmail";
import { routes } from "./values";
import HomeLayout from "./layouts/HomeLayout";

export default function Routing() {
  return (<>
    <Routes>
      <Route path="/*" element={<RootLayout />}>
        <Route element={<SessionLayout />} >
          <Route path="home/*" element={<HomeLayout />}>
            <Route index element={<HomePage />} />
          </Route>
          <Route path="admin/*" element={<AdminLayout />}>
            <Route index element={<HomePage />} />
          </Route>
        </Route>
        <Route path="auth/*" element={<AuthLayout />}>
          <Route path={routes.getPageRouteName(routes.auth.login)} element={<LoginPage />} />
          <Route path={routes.getPageRouteName(routes.auth.register)} element={<RegisterPage />} />
          <Route path={routes.getPageRouteName(routes.auth.forgotPassword)} element={<ForgotPasswordPage />} />
          <Route path={routes.getPageRouteName(routes.auth.resetPassword)} element={<ResetPasswordPage />} />
          <Route path={`${routes.getPageRouteName(routes.auth.verifyEmail)}`} element={<VerifyEmailPage />} />
        </Route>
      </Route>
    </Routes>
  </>)
}