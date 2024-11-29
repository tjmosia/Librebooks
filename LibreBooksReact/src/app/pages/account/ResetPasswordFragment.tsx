import { usePageTitle } from "../../../hooks";

export default function ResetPasswordFragment() {
    usePageTitle("Reset Password")
    return (
        <form
            className='animate__animated animate__fadeIn'>
            <h1>Hello from Reset Password</h1>
        </form>
    )
}