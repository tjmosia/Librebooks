import { createContext, type Dispatch, type SetStateAction } from "react";
import type { IClaimsPrincipal } from "../core/identity";

interface IIdentityContext {
    claimsPrincipal?: IClaimsPrincipal,
    setClaimsPrincipal: Dispatch<SetStateAction<IClaimsPrincipal | undefined>>
}

const IdentityContext = createContext<IIdentityContext>({
    setClaimsPrincipal: () => { }
})

export {
    IdentityContext,
    type IIdentityContext
};
