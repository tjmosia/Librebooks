import { useContext } from "react";
import { IClaim } from "../core/identity";
import { UserManagerContext } from "../contexts";

function userManager(){
    const userContext = useContext(UserManagerContext)!

    return {
        getUser: userContext.user,
        getClaims: userContext.claims,
        getRoles: userContext.roles,
        signOut: () => userContext.clearUserData,
        addRoles: (roles: string[]) => userContext.addRoles!(roles),
        addClaims: (claims: IClaim[]) => userContext.addClaims!(claims),
        addUser: userContext.addUser,
        signIn: userContext.addUser,
        isSignedIn: () => userContext.user != null
    }
}

export default userManager