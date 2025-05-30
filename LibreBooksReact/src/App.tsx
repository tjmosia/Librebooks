import './App.scss'
import Routing from './routing'
import { BrowserRouter } from 'react-router'
import { BlueprintProvider } from "@blueprintjs/core";
import { IUserData, IUserManagerContext, UserManagerContext } from './contexts/UserManagerContext';
import { IClaim, IUser } from './core/identity';
import { useState } from 'react';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { CompanyManagerContext, ICompanyData, ICompanyManagerContext } from './contexts';

const queryClient = new QueryClient()

function App() {
    const [user, setUser] = useState<IUserData | null>({
        email: "tjmosia@outlook.com",
        firstName: "Thabo",
        lastName: "Mosia",
        photo: null
    })

    const [roles, setRoles] = useState<string[] | null> (null)
    const [claims, setClaims] = useState<IClaim[] | null> (null)
    const [company, setCompany] = useState<ICompanyData | null>(null)

    const userManagerContext: IUserManagerContext  = {
        user,
        roles,
        claims,
        addClaims: (claims: IClaim[]) => setClaims(claims),
        addRoles: (roles: string[]) => setRoles(roles),
        addUser: (user: IUserData) => setUser(user),
        clearUserData: () => {
            setClaims(null)
            setRoles(null)
            setUser(null)
        },
        removeClaims: () => setClaims(null),
        removeRoles: () => setRoles(null),
        removeUser: () => setUser(null)
    }

    const companyManagerContext : ICompanyManagerContext = {
        addCompany: (company: ICompanyData) => setCompany(company),
        removeCompany: () => setCompany(null),
        company
    }

    return (
        <>
            <BlueprintProvider>
                <BrowserRouter>
                    <QueryClientProvider client={queryClient}>
                        <UserManagerContext.Provider value={userManagerContext}>
                            <CompanyManagerContext.Provider value={companyManagerContext}>
                                <Routing />
                            </CompanyManagerContext.Provider>
                        </UserManagerContext.Provider>
                    </QueryClientProvider>
                </BrowserRouter>
            </BlueprintProvider>
        </>
    )
}

export default App
