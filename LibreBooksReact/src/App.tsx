import './App.scss'
import Routing from './routing'
import { BrowserRouter } from 'react-router'
import { BlueprintProvider } from "@blueprintjs/core";
import { useUserManager } from './hooks';
import UserManagerContext, { IUserManagerContext } from './contexts/AuthContext';
import { IClaim, IUser } from './core/identity';
import { useState } from 'react';

function App() {
    const [user, setUser] = useState<IUser | null>(null)
    const [roles, setRoles] = useState<string[]> ([])
    const [claims, setClaims] = useState<IClaim[]> ([])


    const userManagerContext: IUserManagerContext  = {
        user: user,
        roles: [],
        claims: [],
        addRoles: (roles: string[]) => setRoles(roles),
        addClaims: (claims: IClaim[]) => setClaims(claims),
        signIn: (user: IUser) => setUser(user),
        signOut: () => setUser(null)
    }

    return (
        <>
            <BlueprintProvider>
                <BrowserRouter>
                    <UserManagerContext.Provider value={userManagerContext}>
                            <Routing />
                    </UserManagerContext.Provider>
                </BrowserRouter>
            </BlueprintProvider>
        </>
    )
}

export default App
