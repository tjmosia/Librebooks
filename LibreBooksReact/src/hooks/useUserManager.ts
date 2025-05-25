import { useContext } from "react";
import UserManagerContext from "../contexts/AuthContext";
import { IClaim } from "../core/identity";


function userManager(){
    const userContext = useContext(UserManagerContext)

    function signOut(){
        userContext.signOut!()
    }

    function signIn(){
        userContext.signIn
    }

    return {
        getUser: () => userContext.user,
        getRoles: () => userContext.roles,
        isSignedIn: () => userContext.user != null,
        signOut,
        signIn,
        getClaims: () => userContext.claims,
        addRoles: (roles: string[]) => userContext.addRoles!(roles),
        addClaims: (claims: IClaim[]) => userContext.addClaims!(claims),
    }
}



export default userManager