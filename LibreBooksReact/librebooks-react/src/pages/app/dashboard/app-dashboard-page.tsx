import AuthorizeViewGuard from "../../../guards/authorizeViewGuard.tsx";


export default  function AppDashboardPage() {

    return (
        <AuthorizeViewGuard roles={["admin"]}>
            <div>
                Dashboard Works
            </div>
        </AuthorizeViewGuard>
    )
}