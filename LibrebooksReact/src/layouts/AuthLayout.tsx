import { Outlet } from "react-router"
import './AuthLayout.scss'
import { Elevation, EntityTitle, H4, Navbar, Section, SectionCard } from "@blueprintjs/core"
import { useState } from "react"
import { AuthContext } from "../app/identity/auth/AuthContext"

export default function AuthLayout() {
    const [title, setTitle] = useState<string>("Auth")
    const [subTitle, setSubTitle] = useState<string>("")
    const [loading, setLoading] = useState<boolean>(false)

    const context = {
        setTitle: (title: string) => setTitle(title),
        setSubTitle: (subTitle: string) => setSubTitle(subTitle),
        loading: loading,
        setLoading: (loading: boolean) => setLoading(loading)
    }

    return (
        <>
            <div className="auth layout">
                <Navbar></Navbar>
                <div className="auth-content">
                    <Section className="animate__animated animate__fadeIn" title={<EntityTitle title={title} heading={H4} />} subtitle={subTitle} elevation={Elevation.ONE}>
                        <SectionCard>
                            <AuthContext.Provider value={context}>
                                <Outlet />
                            </AuthContext.Provider>
                        </SectionCard>
                    </Section>
                </div>
            </div>
        </>
    )
}