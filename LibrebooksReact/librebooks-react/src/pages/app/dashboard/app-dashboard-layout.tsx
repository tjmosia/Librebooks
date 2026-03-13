import AuthorizeViewGuard from "../../../guards/authorizeViewGuard.tsx";


export default function AppDashboardLayout() {

    return (
        <AuthorizeViewGuard >
            <div>
                Dashboard Layout Works
            </div>
        </AuthorizeViewGuard>
    )
}